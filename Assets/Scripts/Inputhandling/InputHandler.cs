using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Enum to distinguish game-modes for inputhandling.
public enum GameInputType { KV,GateBuilder,Plotter,None,Menu,DecimalEncoder,Intro}

/**
 * Class to centrally handle inputs. 
 * Contains all inputbehaviour and activates or deactivates input-behaviour depending on the current game-mode.
 * Uses Singleton pattern to only have one input-handling throughout the game.
 */ 
[CreateAssetMenu(menuName = "Manager/InputHandler")]
public class InputHandler : SingletonScriptableObject<InputHandler>
{

    // Reference to unity-generated input-behaviour-script.
    private InputMasterCompSys _inputs;

    // Current Gametype used for input-behaviour-switching.
    [SerializeField] private GameInputType _currentGIT;

    // General references for inputbehaviour on multiple scenes. 
    [SerializeField] private GameObjectSO selected;
    [SerializeField] private GameObjectSO _camera;
    [SerializeField] private GridSO _mainGrid;
    [SerializeField] private GridSO _auxGrid;
    [SerializeField] private GameObjectSO _pausemenu;
    public AudioClip[] clips = new AudioClip[5];

    // References used for Input-Behaviour on Powercity.
    [SerializeField] private GameObjectSO _filler;
    [SerializeField] private float _scrollMin = 50;
    [SerializeField] private float _scrollMax = 250;
    [SerializeField] private GameObjectSO _previewObject;
    private Vector2Int tempTargetCoord;
    [SerializeField] private bool _selectionBlocked = false;
    private int _incomingSocketsToPlace = 0;
    private int _outgoingSocketsToPlace = 0;
    [SerializeField] private GameObject _incomingSocketPrefab;
    [SerializeField] private GameObject _outgoingSocketPrefab;
    [SerializeField] private Material _previewMat;
    private bool _connectionplacement = false;
    [SerializeField] private GameObjectSO _connection;
    public GameObject floatingText;
    public GameObjectSO selectionPanel;

    // References used for Input-Behaviour on Number Push.
    [SerializeField] public GameObjectSO _player;
    [SerializeField] private GameObjectSO _temp;
    [SerializeField] private GameObjectSO _UIManager;

    // On Load, reset all functions to the current game mode and reset variables to avoid currupt states.
    public void OnEnable()
    {
        SetFunctions();
        _incomingSocketsToPlace = 0;
        _outgoingSocketsToPlace = 0;
        _selectionBlocked = false;
    }
    // On Disable, reset all input-behaviour.
    public void OnDisable()
    {
        ResetFunctions();
    }

    // Function to change the Gametype. This must be called by the new gamemode to deactivate old input-behaviour and activate current input-behaviour.
    public void ChangeGIT(GameInputType targetType)
    {
        ResetFunctions();
        this._currentGIT = targetType;
        SetFunctions();
    }

    /**
     * Activates Input-behaviour depending on the current Game-Mode. 
     * The Input-Actionmap must contain all needed entrys and all methods used on these actions must be added to the corresponding event.
     * Actionmap GateBuilder contains the ACtion ClickAction, which triggers on left-mouse-button click. 
     * On this Action, the Method PlaceSelected will be added, to be invoked on the ClickAction-Event.
     * The GateBuilder-Actionmap will be activated, if the current Gamemode (GameInputType) is GateBuilder.
     */
    private void SetFunctions()
    {
        // Get a Inputmaster, if there is none.
        if(_inputs == null)
        {
            _inputs = new InputMasterCompSys();
        }
        // Reset the selected ScriptableObjectVariable to avoid errors by placing gridelements from the wrong scenes into the current.
        selected.go = null;
        if (_currentGIT == GameInputType.GateBuilder)
        {
            _inputs.Gatebuilder.Enable();
            _inputs.Gatebuilder.ClickAction.performed += PlaceSelected;
            _inputs.Gatebuilder.Rotate.performed += RotateSelected;
            _inputs.Gatebuilder.ChangeCharge.performed += ChangeCTCharge;
            _inputs.Gatebuilder.Cancel.performed += Cancel;
        } 
        if(_currentGIT == GameInputType.KV)
        {
            _inputs.KV.Enable();
            _inputs.KV.SetElement.performed += SetElement;
            _inputs.KV.Cancel.performed += Cancel;
        }
        if(_currentGIT == GameInputType.None)
        {
            _inputs.KV.Enable();
            _inputs.KV.Cancel.performed += Cancel;
        }
        if(_currentGIT == GameInputType.Menu)
        {
            _inputs.Gatebuilder.Enable();
            _inputs.Gatebuilder.Cancel.performed += EndGame;
            
        }
        if(_currentGIT == GameInputType.Plotter)
        {
            _inputs.Schaltnetz.Enable();
            _inputs.Schaltnetz.Cancel.performed += Cancel;
            _inputs.Schaltnetz.MoveCamera.performed += MoveCamera;
            _inputs.Schaltnetz.ClickAction.performed += LeftClickPC;
            _inputs.Schaltnetz.Scroll.performed += ScrollMouse;
            _inputs.Schaltnetz.PreviewBuilding.performed += PreviewOnLocation;
            _inputs.Schaltnetz.Remove.performed += Remove;
            _inputs.Schaltnetz.StopClickAction.performed += StopPlacing;
            _inputs.Schaltnetz.StopRemove.performed += StopRemoving;
        }
        if(_currentGIT == GameInputType.DecimalEncoder)
        {
            _inputs.DecimalEncoder.Enable();
            _inputs.DecimalEncoder.MoveCamera.performed += RotateCamera;
            _inputs.DecimalEncoder.Movement.performed += MovePlayer;
            _inputs.DecimalEncoder.RotatePlayer.performed += RotatePlayer;
            _inputs.DecimalEncoder.Activate.performed += Activate;
            _inputs.DecimalEncoder.Cancel.performed += Cancel;
        }
    }
    /*
     * Reset the Inputmaster so that there is no Inputbehaviour activated and all delegates are removed.
     * Is used on Scene-Switches to change Input-behaviour on different scenes.
     */
    private void ResetFunctions()
    {
        if (_currentGIT == GameInputType.GateBuilder)
        {
            _inputs.Gatebuilder.ClickAction.performed -= PlaceSelected;
            _inputs.Gatebuilder.Rotate.performed -= RotateSelected;
            _inputs.Gatebuilder.ChangeCharge.performed -= ChangeCTCharge;
            _inputs.Gatebuilder.Cancel.performed -= Cancel;
            _inputs.Gatebuilder.Disable();
        }
        if (_currentGIT == GameInputType.KV)
        {
            _inputs.KV.SetElement.performed -= SetElement;
            _inputs.KV.Cancel.performed -= Cancel;
            _inputs.KV.Disable();
        }
        if (_currentGIT == GameInputType.None)
        {
            _inputs.KV.Cancel.performed -= Cancel;
            _inputs.KV.Disable();
        }
        if (_currentGIT == GameInputType.Menu)
        {
            _inputs.Gatebuilder.Cancel.performed -= EndGame;
            _inputs.Gatebuilder.Disable();
        }
        if (_currentGIT == GameInputType.Plotter)
        {
            _inputs.Schaltnetz.Cancel.performed -= Cancel;
            _inputs.Schaltnetz.MoveCamera.performed -= MoveCamera;
            _inputs.Schaltnetz.ClickAction.performed -= LeftClickPC;
            _inputs.Schaltnetz.PreviewBuilding.performed -= PreviewOnLocation;
            _inputs.Schaltnetz.Scroll.performed -= ScrollMouse;
            _inputs.Schaltnetz.Remove.performed -= Remove;
            _inputs.Schaltnetz.StopRemove.performed -= StopRemoving;
            _inputs.Schaltnetz.StopClickAction.performed -= StopPlacing;
            _inputs.Schaltnetz.Disable();
        }
        if (_currentGIT == GameInputType.DecimalEncoder)
        {
            _inputs.DecimalEncoder.MoveCamera.performed -= RotateCamera;
            _inputs.DecimalEncoder.Cancel.performed -= Cancel;
            _inputs.DecimalEncoder.RotatePlayer.performed -= RotatePlayer;
            _inputs.DecimalEncoder.MoveCamera.performed -= MovePlayer;
            _inputs.DecimalEncoder.RotatePlayer.performed -= Activate;
            _inputs.DecimalEncoder.Disable();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Trackymania Input-Behaviour Methods
    /*
     * Function to place a selected Track onto the grid in the scene.
     * Is triggered by left-mouse interaction on GateBuilder ActionMap and ClickAction Action.
     * @param   context Inputcontext given bei the Actionevent. This is not needed for further calculations, but is neccessary to subscribe to inputevents.
     */ 
    private void PlaceSelected(InputAction.CallbackContext context)
    {
        // Fire a ray from the Camera through the Cursor to determine which Object was clicked.
        Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        // Hit information of the raycast.
        RaycastHit hit;
        // Continue only if the Raycast hit an object in the scene.
        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the Coordinates of the GameObject hit by the Raycast.
            Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
            // Check if the hit element is a Gridelement.
            GridElement selection = hit.collider.gameObject.GetComponent<GridElement>();

            // If the hit Object has valid coordinates, it must be on the grid. Place the selected Object onto the grid on the calculated coordinates.
            if (temp.x >= 0)
            {
                // If the player has a grid element in the inventory, place it on the gameboard and play a sound.
                if (selected.go != null)
                {
                    AudioManager.instance.PlaySound(clips[4]);
                    Placer.PlaceObject(_mainGrid.grid, selected.go, temp.x, temp.y);
                }
            }

            // If the Coordinates of the hit object are not valid, check if the hit object is a gridelement. 
            // This must mean the player has selected a grid element for the inventory.
            else if (selection != null)
            {
                // To add the newly chosen element to the inventory, it must be removeable.
                // This must be done, to remove it from the inventory, in case the player chooses another element for the inventory in the future.
                selection.isRemoveable = true;
                selected.go = Placer.PlaceObject(_auxGrid.grid, selection.gameObject, 0, 0);
            }
        }
    }
    /*
     * Rotates the gridelement which is currently in the inventory-slot.
     * @param   context eventcontextdata of the event that triggered the method to be called. This is not relevant for the calculations.
     */ 
    private void RotateSelected(InputAction.CallbackContext context)
    {
        // If there is an element in the inventory and it is rotateable, rotate it.
        if (selected.go != null)
        {
            if (selected.go.GetComponent<IRotateable>() != null)
            {
                selected.go.GetComponent<IRotateable>().Rotate();
            }
        }
    }
    /*
     * While having a barrier-track in the inventory, this method will change the connected variable.
     * @param   context Inputeventcontextdata which holds information about the button that triggered the method call. Used to determine the chosen variable.
     */ 
    private void ChangeCTCharge(InputAction.CallbackContext context)
    {
        // Check if there is a barrier-track in the inventory.
        ConditionalTrack temp = null;
        if (selected.go != null)
        {
            temp = selected.go.GetComponent<ConditionalTrack>();
        }

        // If there is a barrier-track in the inventory, parse the contextdata to determine which variable was chosen.
        if (temp != null)
        {
            // Parses the pressed button to an int. Key "1" = 1, Key "2" = 2,...
            int.TryParse(context.control.name.ToString(), out int choice);
            // Change the variable of the barrier track with the parsed int. (1 = A, 2 = B, 3 = C...)
            temp.ChangeCharge(choice);
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////
    // Harvest Bool Input-Behaviour Methods

    public void SetElement(InputAction.CallbackContext context)
    {
        Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
        KVelement selection = hit.collider.gameObject.GetComponent<KVelement>();
        UIClickable tempClickable = hit.collider.gameObject.GetComponent<UIClickable>();
        if (temp.x >= 0)
        {
            if (selection != null)
            {
                AudioManager.instance.PlaySound(clips[1]);
                selection.SetSetting();
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Powercity Input-Behaviour Methods

    /*
     * Method to increase or decrease the orthographic size of the camera-windows, thus zooming in or out.
     * @param   context Actionevent-context that triggered the method. In this case a move of the mousewheel. 
     * This returns a Vector2 that is used to determine the dicretion of the mouse-wheel-scroll.
     */ 
    private void ScrollMouse(InputAction.CallbackContext context)
    {
        // If the Vector2.y value is smaller than 0, meaning the mouse-wheel was scrolled backwards, zoom out. Otherwise, zoom in.
        // If the max or min zoomfactor is hit, do stop zooming.
        if (context.ReadValue<Vector2>().y < 0)
        {
            if (_camera.go.GetComponent<Camera>().orthographicSize + 5 <= _scrollMax)
            {
                _camera.go.GetComponent<Camera>().orthographicSize += 5;
            }
        }
        else
        {
            if (_camera.go.GetComponent<Camera>().orthographicSize - 5 >= _scrollMin)
            {
                _camera.go.GetComponent<Camera>().orthographicSize -= 5;
            }
        }
    }

    /*
     * Method to handle left-mouse clicks. These can lead to various cases, depending on the clicked Object in the scene.
     * @param   context Context of the Actionevent. In this case the method is triggered by leftclick. The information about the event are not used further.
     */ 
    private void LeftClickPC(InputAction.CallbackContext context)
    {
        // First, cast a ray from the camera through the mouse-cursor and check for hits.
        Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        // If an object was hit, check various cases.
        if (Physics.Raycast(ray, out hit))
        {
            // Get the coordinates of the hit object in the grid.
            Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);

            // If the hit object in the grid is a multiCellElement, show it's truthtable in the selection UI-Panel and return.
            if (_mainGrid.grid.GetObjFromCoordinate(temp).GetComponent<MultiCellElement>())
            {
                FillSelectionResultTable(_mainGrid.grid.GetObjFromCoordinate(temp).GetComponent<MultiCellElement>());
                return;
            }
            // If anything else was hit, hide the selection UI-panel.
            else
            {
                selectionPanel.go.SetActive(false);
            }


            // If the player hasn't selected a specific object to place, start placing connections as long as the left-mouse button is held down.
            // This is done by changing input-behaviour to place a connection every frame, that the mouse has moved.
            // Either the player has already placed a connection-element, in that case only the input-behaviour has to be changed. 
            // Otherwise, the connection-element has to be placed in the inventory.
            if (!selected.go)
            {
               _inputs.Schaltnetz.PreviewBuilding.performed += PlaceConnection;
                selected.go = _connection.go;
                UpdatePreview();
            }
            else
            if (selected.go == _connection.go)
            {
                _inputs.Schaltnetz.PreviewBuilding.performed += PlaceConnection;
            }


            // If the player has something in the inventory, that is not null or a connection-element, takes this route.
            if (temp.x >= 0)
            {
                // Check if the player still has to place incoming or outgoingsockets. If so, try to place the correct element on the clicked spot.
                // If there are no left sockets left to place, skip this part

                // If there are sockets to place, first the incoming, then the outgoing sockets are going to be placed. 
                if (_incomingSocketsToPlace > 0 || _outgoingSocketsToPlace > 0)
                {
                    if (_incomingSocketsToPlace > 0)
                    {
                        PlaceIncomingSocket(temp, tempTargetCoord, _filler.go);
                        // After placing the last incoming socket, change the element in the inventory for the next placement.
                        if (_incomingSocketsToPlace <= 0)
                        {
                            selected.go = _outgoingSocketPrefab;
                            UpdatePreview();
                        }
                    }
                    // After all incoming sockets have been placed, place all outgoing sockets.
                    else
                    {
                        PlaceOutgoingSocket(temp, tempTargetCoord, _filler.go);
                        // When all outgoingsockets are placed, reset the inventory and unlock the selection.
                        if (_outgoingSocketsToPlace <= 0)
                        {
                            _selectionBlocked = false;
                            selected.go = null;
                            UpdatePreview();
                        }
                    }
                }

                // This section handles the case, that the player wants to place an element, that is not a connection and has no sockets left to place.
                else
                {
                    // Variable used to store a reference to the newly placed element.
                    GameObject tempObj;

                    // Give the player auditive feedback about the placement.
                    AudioManager.instance.PlaySound(clips[0]);

                    // If the element in the inventory only is 1x1 gridcells big, just place it on the grid.
                    if (selected.go.GetComponent<GridElement>().size.x == 1 && selected.go.GetComponent<GridElement>().size.y == 1)
                    {
                        // Place the object and take a reference.
                        tempObj = Placer.PlaceObject(_mainGrid.grid, selected.go, temp.x, temp.y, _filler.go);
                    }
                    // Bigger elements need to be placed differently, since there can happen multiple conflicts while placing them on the grid.
                    else
                    {
                        // Place the object and take a reference.
                        tempObj = Placer.PlaceMultiCellObject(_mainGrid.grid, selected.go, temp.x, temp.y, _filler.go);

                        // Bigger elements get a text to identify them on the board. They get a Tag with the given name that is placed right above them.
                        if (tempObj.GetComponentInChildren<TMP_Text>())
                        {
                            tempObj.GetComponentInChildren<TMP_Text>().text = tempObj.GetComponent<MultiCellElement>().name;
                        }
                    }

                    // After placing an object, check if the object needs to have incoming or outgoing sockets. If so, set the counters and lock the selection.
                    // This way, the player will have to place all remaining sockets before being able to place more elements.
                    if (tempObj && (tempObj.GetComponent<GridElement>().incomingSocketCount > 0 || tempObj.GetComponent<GridElement>().outComingSocketCount > 0))
                    {
                        // Block the player from placing other elements, while having to place sockets.
                        _selectionBlocked = true;

                        // Save the coordinates of the newly placed object for when the incoming and outgoing sockets are being planced.
                        tempTargetCoord = temp;

                        // Increase the counter to show how many sockets need to be placed.
                        _incomingSocketsToPlace = tempObj.GetComponent<GridElement>().incomingSocketCount;
                        _outgoingSocketsToPlace = tempObj.GetComponent<GridElement>().outComingSocketCount;

                        // Start by placing incoming sockets by filling the inventory with an incoming socket.
                        selected.go = _incomingSocketPrefab;

                        // Update the preview, so the player can see where the element would be placed.
                        UpdatePreview();
                    }
                }
            }
        }
    }

    /*
     * This functions places an incoming socket and adjusts it's parameters to react correctly when being visited.
     * @param   coords      The coordinates to place the socket on the grid.
     * @param   targetCoord The coordinates of the logic-element to which this sockets belongs to.
     * @param   filler      The filler-object that is used to fill spaces, in case the placement of the sockets causes a conflict with a multi-cell-element.
     */ 
    private void PlaceIncomingSocket(Vector2Int coords, Vector2Int targetCoords, GameObject filler)
    {
        // Only place the socket, if the coords is adjacent to the logic-element.
        if (Placer.IsAdjacent(_mainGrid.grid, coords, targetCoords))
        {
            // Get a reference to the newly placed socket.
            GameObject tempObj;
            tempObj = Placer.PlaceObject(_mainGrid.grid, selected.go, coords.x, coords.y, _filler.go);

            // If the placement was successfull, play a sound and adjust attributes of the socket and the logic-element.
            if (tempObj)
            {
                // Auditive feedback for the successfull placement.
                AudioManager.instance.PlaySound(clips[0]);
                // Set the target-logic-element coordinates to the saved coordinates.
                tempObj.GetComponent<SocketOfGridElement>().coordsToTarget = tempTargetCoord;
                // Set the target-logic-element to be the target of the socket.
                tempObj.GetComponent<SocketOfGridElement>().targetGo = _mainGrid.grid.GetObjFromCoordinate(tempTargetCoord);
                // Add this socket to the list of incoming sockets of the logic-element.
                MultiCellElement targetMCE = _mainGrid.grid.GetObjFromCoordinate(tempTargetCoord).GetComponent<MultiCellElement>();

                // Nullchecking the element. Might not really be neccessary, if the inventory is correct, which it should.
                if (tempObj.GetComponentInChildren<TMP_Text>())
                {
                    // Set the text of the prefab to the corresponding variable it resembles.
                    // The first incoming socket is always A, the second is B, the third is C and so on.
                    tempObj.GetComponentInChildren<TMP_Text>().text = ((Charge)targetMCE.incomingSockets.Count + 1).ToString();
                }
                targetMCE.AddToList(targetMCE.incomingSockets, coords);

                // Decrement the number of incoming sockets needed to be placed by the player.
                _incomingSocketsToPlace--;

            }
        }
        // Show a message, reminding the player that sockets need to be placed adjacent to the element.
        else
        {
            floatingText.GetComponent<Animation>().Play();
        }
    }

    /*
     * Similar to placing an incoming socket, but still different.
     * @param   coords      The coordinates to place the socket on the grid.
     * @param   targetCoord The coordinates of the logic-element to which this sockets belongs to.
     * @param   filler      The filler-object that is used to fill spaces, in case the placement of the sockets causes a conflict with a multi-cell-element.  
     */
    private void PlaceOutgoingSocket(Vector2Int coords, Vector2Int targetCoords, GameObject filler)
    {
        // Also outgoing sockets can only be placed adjacent to logic-elements.
        if (Placer.IsAdjacent(_mainGrid.grid, coords, targetCoords))
        {

            // Get a reference to the newly placed socket.
            GameObject tempObj;
            tempObj = Placer.PlaceObject(_mainGrid.grid, selected.go, coords.x, coords.y, _filler.go);


            // If the placement was successfull, play a sound and adjust attributes of the socket and the logic-element.
            if (tempObj)
            {
                AudioManager.instance.PlaySound(clips[0]);

                // Since outgoingsockets can't be visited, we only need to add the socket to the logic-elements list of outgoing sockets.
                // There is no need to define a target for the outgoing socket.
                MultiCellElement targetMCE = _mainGrid.grid.GetObjFromCoordinate(tempTargetCoord).GetComponent<MultiCellElement>();
                if (tempObj.GetComponentInChildren<TMP_Text>())
                {
                    // Set the text of the prefab to the corresponding variable it resembles.
                    // The first outgoing socket is always 0, the second is 1, the third is 2 and so on.
                    tempObj.GetComponentInChildren<TMP_Text>().text = targetMCE.outgoingSockets.Count.ToString();
                }
                targetMCE.AddToList(targetMCE.outgoingSockets, coords);
                targetMCE.outgoingSocketObjects.Add(tempObj.GetComponent<OutGoingSocket>());


                // Decrement the number of incoming sockets needed to be placed by the player.
                _outgoingSocketsToPlace--;

            }
        }
        // Show a message, reminding the player that sockets need to be placed adjacent to the element.
        else
        {
            floatingText.GetComponent<Animation>().Play();
        }
    }

    // Changes the current element in the inventory to a new game-object (go).
    public void SelectNewElement(GameObject go)
    {
        // Only change the selection, if the selection is not blocked.
        if (!_selectionBlocked)
        {
            selected.go = go;
            // Update the preview-element, so the player can see where to place the object.
            UpdatePreview();
        }
    }

    /*
     * Starts removing grid-elements that are below the cursor, by changeing input-behaviour. 
     * In this case, on each mouse movement and while the right mouse is held down, the element below the cursor is removed.
     * @param   context The information provided with the click-event. In this case the right-mouse-button click event.
     */ 
    public void Remove(InputAction.CallbackContext context)
    {
        _inputs.Schaltnetz.PreviewBuilding.performed += RemoveMore;
    }

    /*
     * If the player releases the right mouse button, the last grid-element is being removed and the input-behaviour is reset.
     * @param   context The information provided with the triggering event. In this case the release of the right-mouse-button.
     */ 
    public void StopRemoving(InputAction.CallbackContext context)
    {
        // Before resetting the input-bevaviour to not remove any object, after the mouse moved, remove the last element which was hit.

        // Placing and removing objects is only allowed, when the selection is not blocked.
        // This indicates that there is no socket left to place for a logic-element.
        if (!_selectionBlocked)
        {
            // Cast a ray from the camera through the cursor and identify the hit object.
            Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // If any object has been hit, remove it from the grid and play a feedback sound.
            if (Physics.Raycast(ray, out hit))
            {
                // Play a sound through to indicate a successfull removal.
                AudioManager.instance.PlaySound(clips[0]);

                // Identify the hit by it's coordinates in the grid.
                Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);

                // Place a filler-element on the identified coordinates, thusly "removing" the element.
                Placer.PlaceObject(_mainGrid.grid, _filler.go, temp.x, temp.y, _filler.go);
            }
        }

        // After the last element has been removed, reset to normal input-bevahiour
        _inputs.Schaltnetz.PreviewBuilding.performed -= RemoveMore;
    }

    /*
     * Method to be added to the input-behaviour when removing objects is needed. This will be done by the Remove-Method.
     * After adding this method to the input-behaviour, it will be triggered every frame the mouse has moved, removing all objects the mouse has hit.
     * @param   context Informationen provided with the triggering event. In this case the movement of the mouse.
     */ 
    public void RemoveMore(InputAction.CallbackContext context)
    {
        // Placing and removing objects is only allowed, when the selection is not blocked.
        // This indicates that there is no socket left to place for a logic-element.
        if (!_selectionBlocked)
        {
            // Cast a ray from the camera through the cursor and get a refenrence to the hit object, if there is one.
            Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // If an object was hit, try removing it. This will be successfull, if it is a removeable grid-element.
            if (Physics.Raycast(ray, out hit))
            {
                // Play a sound to indicate a successfull removal.
                AudioManager.instance.PlaySound(clips[0]);

                // Get the coordinates of the hit object in the grid.
                Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);

                // Remove the hit object by placing a filler-element on it's coordinates.
                Placer.PlaceObject(_mainGrid.grid, _filler.go, temp.x, temp.y, _filler.go);
            }
        }
    }

    /*
     * Starts moving the camera by invoking the move-method on the camera.
     * @param   context The information provided by the triggering event. This is a vector2, calculated by the buttons pressed and the corresponding axis.
     * W- and S-keys determine the y-Axis, while A- and D-keys determine the x-Axis.
     */ 
    public void MoveCamera(InputAction.CallbackContext context)
    {
        // Nullchecking for the MoveCamera-Component on the main-Camera-Gameobject.
        MoveCamera temp = _camera.go.GetComponent<MoveCamera>();
        if (temp)
        {
            Vector3 tempVec = context.ReadValue<Vector2>();
            temp.Move(tempVec);
        }
    }

    // Stops autoplacing connections. This is triggered by releasing the left mouse-button, after placing a connection.
    private void StopPlacing(InputAction.CallbackContext context)
    {
        _inputs.Schaltnetz.PreviewBuilding.performed -= PlaceConnection;
    }

    /*
     * After being added to the input-behaviour, this method is called every frame the mouse has moved and is used to continously place connections,
     * while the left mouse-button is being held down.
     * @param   context Information provided by the triggering event. In this case the movement of the mouse.
     */ 
    private void PlaceConnection(InputAction.CallbackContext context)
    {
        // Cast a ray from the camera through the cursor and keep a reference to the hit object.
        Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // If an object was hit, place a connection-element on it's place.
        if (Physics.Raycast(ray, out hit))
        {
            // Get the coordinates of the hit grid-element, if it is one.
            Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
            // Place a connection element on the hit grid-element.
            Placer.PlaceObject(_mainGrid.grid, selected.go, temp.x, temp.y, _filler.go);
        }
    }

    /*
     * On logic-elements, it can be useful to see it's truthtable.
     * This method is called by various input-behaviours and displays the truthtable of the clicked or selected logic-element.
     * @param   mceObject   The MultiCellElement of which to show the truthtable. If this is null, show the truthtable of the element in the inventory.
     */ 
    public void FillSelectionResultTable(MultiCellElement mceObject = null)
    {
        // If no object was given, get the MultiCellElement-Component of the element in the inventory.
        if (mceObject == null)
        {
            mceObject = selected.go.GetComponent<MultiCellElement>();
        }

        // Fill the selectionpanel with the truthtable of the determined MCE (MultiCellElement) if it is presentable.
        if (mceObject && mceObject.isPresentable)
        {
            UITableFiller.FillTable(selectionPanel.go, mceObject.resultList, mceObject.outComingSocketCount, mceObject.incomingSocketCount, mceObject.resultList, width: 300, height: 205);
            selectionPanel.go.SetActive(true);
        }
    }


    /*
     * Show a preview of the currently chosen element on the grid, to indicate where the selected element would be placed on the grid.
     * This is done by creating an object of the same size as the selected element and placing it right above the grid. 
     * @param   context Information provided from the triggering event. In this case the movement of the mouse. 
     */ 
    private void PreviewOnLocation(InputAction.CallbackContext context)
    {
        // The first case is used, if the player has an element in the inventory, but a preview-object hasn't been build yet.
        if (!_previewObject.go && selected.go)
        {
            // Create a preview-object, by copying the element in the inventory and scaling it to accomidate the right cellsize.
            _previewObject.go = Instantiate(selected.go);
            _previewObject.go.GetComponent<Renderer>().material = _previewMat;
            int x = _previewObject.go.GetComponent<GridElement>().size.x;
            int y = _previewObject.go.GetComponent<GridElement>().size.y;
            float sizeOffsetX = 1 * x / _previewObject.go.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1 * y / _previewObject.go.GetComponent<Renderer>().bounds.size.y;
            _previewObject.go.transform.localScale = new Vector3(_mainGrid.grid.FieldSize * sizeOffsetX, _mainGrid.grid.FieldSize * sizeOffsetY, 1);
        }
        // If there is an preview-object build already, place it right over the calculated coordinates.
        if (_previewObject.go)
        {
            Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
            Placer.PlacePreview(_mainGrid.grid, _previewObject.go, temp.x, temp.y);
        }
    }

    /*
     * Used to update the preview-object on various events.
     */ 
    public void UpdatePreview()
    {
        // If there is an element in the inventory, build a preview-object of it.
        if (selected.go)
        {
            // Delete the previous preview-object if there is one.
            if (_previewObject.go)
            {
                Destroy(_previewObject.go);
            }

            // Create a new preview-object from the element in the inventory and scale it to accomidate the cellsize of the grid.
            _previewObject.go = Instantiate(selected.go);

            _previewObject.go.GetComponent<Renderer>().material = _previewMat;
            int x = _previewObject.go.GetComponent<GridElement>().size.x;
            int y = _previewObject.go.GetComponent<GridElement>().size.y;
            float sizeOffsetX = 1 * x / _previewObject.go.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1 * y / _previewObject.go.GetComponent<Renderer>().bounds.size.y;

            _previewObject.go.transform.localScale = new Vector3(_mainGrid.grid.FieldSize * sizeOffsetX, _mainGrid.grid.FieldSize * sizeOffsetY, 1);
        }
        // If there is no element selected, clear the preview-object so nothing is being previewed on the grid.
        else
        {
            Destroy(_previewObject.go);
            _previewObject.go = null;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Number Push Input-Behaviour Methods

    /*
     * Rotates the camera on the y-axis on specific input-actions. 
     * @param   context Informationen provided by the triggering event. In this case movement of the mouse. It holds a Float-value of the mouse-movement.
     */ 
    private void RotateCamera(InputAction.CallbackContext context)
    {
        // Calls the Rotate-method of the CameraRotation-Component of the main-camera with the given float-value. Thus rotating the camera in the y-Axis.
        _camera.go.GetComponent<CameraRotation>().Rotate(context.ReadValue<float>());
    }


    /*
     * Rotates the player on the x-axis on specific input-actions. 
     * @param   context Informationen provided by the triggering event. In this case movement of the mouse. It holds a Float-value of the mouse-movement.
     */
    private void RotatePlayer(InputAction.CallbackContext context)
    {
        // Calls the Rotate-method of the PlayerRotation-Component of the player with the given float-value. Thus rotating the player in the x-Axis.
        _player.go.GetComponent<PlayerRotation>().Rotate(context.ReadValue<float>());
    }

    /*
     * Moves the player on specific input-actions. 
     * @param   context Informationen provided by the triggering event. In this case pressing the Axis-Buttons (W, A, S & D). The context holds a Vector2.
     * W- and S-keys determine the y-Axis, while A- and D-keys determine the x-Axis.
     */
    private void MovePlayer(InputAction.CallbackContext context)
    {
        // Calls the Rotate-method of the PlayerRotation-Component of the player with the given vector2-value. Thus moving the player with the given vector2.
        _player.go.GetComponent<PlayerMovement>().Move(context.ReadValue<Vector2>());
    }

    /*
     * Activates the currently pointed at Interactable, if there is one. 
     * @param   context Information provided by the triggering event. In this case pressing the E-Button. 
     */ 
    private void Activate(InputAction.CallbackContext context)
    {
        // Check, if there currently is a object, that is being looked at through the camera.
        // This is filled by the CameraTargeting-Script on the player-Camera.
        if (_temp.go)
        {
            // Activate the currently looked at object.
            _temp.go.GetComponent<Interactable>().Activate();
        }
        else
        // If nothing is looked at, drop what's inside the players inventory 2 meters in front of the player and 2 meters high in the air.
        if (selected.go)
        {
            Vector3 temp = _player.go.transform.position+ _player.go.transform.forward*2;
            temp.y = 2;
            selected.go.transform.position = temp;
            selected.go = null;
        }
        // Update the UI in case the player has picked up or dropped something.
        _UIManager.go.GetComponent<UIManager>().UpdateUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Menu Input-Behaviour Methods

    // On ESC-Button press, quit the application.
    private void EndGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // General Input-Behaviour Methods

    /*
     * Cancel method, to either present the Pause menu, or stop placing a logic element on Powercity.
     * @param   context Information provided by the triggering event. In this case the buttonpress on the ESC-key.
     */ 
    private void Cancel(InputAction.CallbackContext context)
    {
        // If the selection is blocked, and therefore the player must be placing sockets to an logic-element in powercity, stop this process.
        if (_selectionBlocked)
        {
            Placer.PlaceObject(_mainGrid.grid, _filler.go, tempTargetCoord.x, tempTargetCoord.y, _filler.go);
            _incomingSocketsToPlace = 0;
            _outgoingSocketsToPlace = 0;
            _selectionBlocked = false;
            selected.go = null;
            return;
        }
        // In any other case, pause the game.
        PauseGame();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Helper-Methods

    /*
     * Pause and unpause the game and show or hide the pause-menu. On Number-Push do additional cursor-management.
     */ 
    public void PauseGame()
    {
        // If the game is paused and the pause menu is shown, hide it and unpause.
        if (_pausemenu.go.activeSelf)
        {
            // On Number push, reactivate movement-behaviour and lock and hide the cursor in the center.
            if (_currentGIT == GameInputType.DecimalEncoder)
            {
                _inputs.DecimalEncoder.RotatePlayer.performed += RotatePlayer;

                _inputs.DecimalEncoder.MoveCamera.performed += RotateCamera;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            _pausemenu.go.SetActive(false);
        }

        // If the game is not paused, show the pause menu and stop movement.
        else
        {
            // On Number push, stop player-movement and rotation, aswell as unlocking and showing the cursor.
            if (_currentGIT == GameInputType.DecimalEncoder)
            {
                _inputs.DecimalEncoder.RotatePlayer.performed -= RotatePlayer;
                _inputs.DecimalEncoder.MoveCamera.performed -= RotateCamera;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            _pausemenu.go.SetActive(true);
        }
    }
}
