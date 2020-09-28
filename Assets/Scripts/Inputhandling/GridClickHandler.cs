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
[CreateAssetMenu(menuName = "Manager/GridClickHandler")]
public class GridClickHandler : SingletonScriptableObject<GridClickHandler>
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
            _inputs.Schaltnetz.ClickAction.performed += TestClick;
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
            _inputs.Schaltnetz.ClickAction.performed -= TestClick;
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


    private void ScrollMouse(InputAction.CallbackContext context)
    {
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

    private void TestClick(InputAction.CallbackContext context)
    {


        Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
            if (_mainGrid.grid.GetObjFromCoordinate(temp).GetComponent<MultiCellElement>())
            {
                FillSelectionResultTable(_mainGrid.grid.GetObjFromCoordinate(temp).GetComponent<MultiCellElement>());
                return;
            }
            else
            {
                selectionPanel.go.SetActive(false);
            }
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
            if (temp.x >= 0)
            {
                if (_incomingSocketsToPlace > 0 || _outgoingSocketsToPlace > 0)
                {
                    if (_incomingSocketsToPlace > 0)
                    {
                        PlaceIncomingSocket(temp, tempTargetCoord, _filler.go);
                        if (_incomingSocketsToPlace <= 0)
                        {
                            selected.go = _outgoingSocketPrefab;
                            UpdatePreview();
                        }
                    }
                    else
                    {
                        PlaceOutgoingSocket(temp, tempTargetCoord, _filler.go);
                        if (_outgoingSocketsToPlace <= 0)
                        {
                            _selectionBlocked = false;
                            selected.go = null;
                            UpdatePreview();
                        }
                    }
                }
                else
                {
                    GameObject tempObj;

                    AudioManager.instance.PlaySound(clips[0]);
                    if (selected.go.GetComponent<GridElement>().size.x == 1 && selected.go.GetComponent<GridElement>().size.y == 1)
                    {
                        tempObj = Placer.PlaceObject(_mainGrid.grid, selected.go, temp.x, temp.y, _filler.go);
                    }
                    else
                    {
                        tempObj = Placer.PlaceMultiCellObject(_mainGrid.grid, selected.go, temp.x, temp.y, _filler.go);
                        if (tempObj.GetComponentInChildren<TMP_Text>())
                        {
                            tempObj.GetComponentInChildren<TMP_Text>().text = tempObj.GetComponent<MultiCellElement>().name;
                        }
                    }
                    if (tempObj && (tempObj.GetComponent<GridElement>().incomingSocketCount > 0 || tempObj.GetComponent<GridElement>().outComingSocketCount > 0))
                    {
                        _selectionBlocked = true;
                        tempTargetCoord = temp;
                        _incomingSocketsToPlace = tempObj.GetComponent<GridElement>().incomingSocketCount;
                        _outgoingSocketsToPlace = tempObj.GetComponent<GridElement>().outComingSocketCount;
                        selected.go = _incomingSocketPrefab;
                        UpdatePreview();
                    }
                }
            }
        }
    }

    private void PlaceIncomingSocket(Vector2Int coords, Vector2Int targetCoords, GameObject filler)
    {
        if (Placer.IsAdjacent(_mainGrid.grid, coords, targetCoords))
        {
            GameObject tempObj;
            tempObj = Placer.PlaceObject(_mainGrid.grid, selected.go, coords.x, coords.y, _filler.go);
            if (tempObj)
            {

                AudioManager.instance.PlaySound(clips[0]);
                tempObj.GetComponent<SocketOfGridElement>().coordsToTarget = tempTargetCoord;
                tempObj.GetComponent<SocketOfGridElement>().targetGo = _mainGrid.grid.GetObjFromCoordinate(tempTargetCoord);
                MultiCellElement targetMCE = _mainGrid.grid.GetObjFromCoordinate(tempTargetCoord).GetComponent<MultiCellElement>();
                if (tempObj.GetComponentInChildren<TMP_Text>())
                {

                    tempObj.GetComponentInChildren<TMP_Text>().text = ((Charge)targetMCE.incomingSockets.Count + 1).ToString();
                }
                targetMCE.AddToList(targetMCE.incomingSockets, coords);

                _incomingSocketsToPlace--;

            }
        }
        else
        {
            floatingText.GetComponent<Animation>().Play();
        }
    }

    private void PlaceOutgoingSocket(Vector2Int coords, Vector2Int targetCoords, GameObject filler)
    {
        if (Placer.IsAdjacent(_mainGrid.grid, coords, targetCoords))
        {
            GameObject tempObj;
            tempObj = Placer.PlaceObject(_mainGrid.grid, selected.go, coords.x, coords.y, _filler.go);
            if (tempObj)
            {
                AudioManager.instance.PlaySound(clips[0]);
                MultiCellElement targetMCE = _mainGrid.grid.GetObjFromCoordinate(tempTargetCoord).GetComponent<MultiCellElement>();
                if (tempObj.GetComponentInChildren<TMP_Text>())
                {

                    tempObj.GetComponentInChildren<TMP_Text>().text = targetMCE.outgoingSockets.Count.ToString();
                }
                targetMCE.AddToList(targetMCE.outgoingSockets, coords);
                targetMCE.outgoingSocketObjects.Add(tempObj.GetComponent<OutGoingSocket>());
                _outgoingSocketsToPlace--;

            }
        }
        else
        {
            floatingText.GetComponent<Animation>().Play();
        }
    }

    public void SelectNewElement(GameObject go)
    {
        if (!_selectionBlocked)
        {
            selected.go = go;
            UpdatePreview();
        }

    }

    public void Remove(InputAction.CallbackContext context)
    {
        _inputs.Schaltnetz.PreviewBuilding.performed += RemoveMore;


    }

    public void StopRemoving(InputAction.CallbackContext context)
    {
        if (!_selectionBlocked)
        {
            Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                AudioManager.instance.PlaySound(clips[0]);
                Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
                Placer.PlaceObject(_mainGrid.grid, _filler.go, temp.x, temp.y, _filler.go);

                AudioManager.instance.PlaySound(clips[0]);
            }
        }
        _inputs.Schaltnetz.PreviewBuilding.performed -= RemoveMore;
    }

    public void RemoveMore(InputAction.CallbackContext context)
    {
        if (!_selectionBlocked)
        {
            Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                AudioManager.instance.PlaySound(clips[0]);
                Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
                Placer.PlaceObject(_mainGrid.grid, _filler.go, temp.x, temp.y, _filler.go);

                AudioManager.instance.PlaySound(clips[0]);
            }
        }
    }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        MoveCamera temp = _camera.go.GetComponent<MoveCamera>();
        if (temp)
        {
            Vector3 tempVec = context.ReadValue<Vector2>();
            temp.Move(tempVec);
        }
    }

    private void StopPlacing(InputAction.CallbackContext context)
    {

        _inputs.Schaltnetz.PreviewBuilding.performed -= PlaceConnection;

    }

    private void PlaceConnection(InputAction.CallbackContext context)
    {
        Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
            Placer.PlaceObject(_mainGrid.grid, selected.go, temp.x, temp.y, _filler.go);
        }
    }

    public void FillSelectionResultTable(MultiCellElement mceObject = null)
    {
        if (mceObject == null)
        {
            mceObject = selected.go.GetComponent<MultiCellElement>();
        }
        if (mceObject && mceObject.isPresentable)
        {
            UITableFiller.FillTable(selectionPanel.go, mceObject.resultList, mceObject.outComingSocketCount, mceObject.incomingSocketCount, mceObject.resultList, width: 300, height: 205);
            selectionPanel.go.SetActive(true);
        }
    }

    private void PreviewOnLocation(InputAction.CallbackContext context)
    {
        if (!_previewObject.go && selected.go)
        {

            _previewObject.go = Instantiate(selected.go);
            _previewObject.go.GetComponent<Renderer>().material = _previewMat;
            int x = _previewObject.go.GetComponent<GridElement>().size.x;
            int y = _previewObject.go.GetComponent<GridElement>().size.y;
            float sizeOffsetX = 1 * x / _previewObject.go.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1 * y / _previewObject.go.GetComponent<Renderer>().bounds.size.y;

            _previewObject.go.transform.localScale = new Vector3(_mainGrid.grid.FieldSize * sizeOffsetX, _mainGrid.grid.FieldSize * sizeOffsetY, 1);

        }
        if (_previewObject.go)
        {
            Ray ray = _camera.go.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Vector2Int temp = _mainGrid.grid.GetCoordinate(hit.point);
            Placer.PlacePreview(_mainGrid.grid, _previewObject.go, temp.x, temp.y);
        }

    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Number Push Input-Behaviour Methods

    private void RotateCamera(InputAction.CallbackContext context)
    {
        _camera.go.GetComponent<CameraRotation>().Rotate(context.ReadValue<float>());
    }

    private void RotatePlayer(InputAction.CallbackContext context)
    {
        
        _player.go.GetComponent<PlayerRotation>().Rotate(context.ReadValue<float>());
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        _player.go.GetComponent<PlayerMovement>().Move(context.ReadValue<Vector2>());
    }

    private void Activate(InputAction.CallbackContext context)
    {

        if (_temp.go)
        {
            _temp.go.GetComponent<Interactable>().Activate();
        } else
        if (selected.go)
        {
            Vector3 temp = _player.go.transform.position+ _player.go.transform.forward*2;
            temp.y = 2;
            selected.go.transform.position = temp;
            selected.go = null;
        }

        _UIManager.go.GetComponent<UIManager>().UpdateUI();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Menu Input-Behaviour Methods


    private void EndGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // General Input-Behaviour Methods

    private void Cancel(InputAction.CallbackContext context)
    {
        if (_selectionBlocked)
        {
            Placer.PlaceObject(_mainGrid.grid, _filler.go, tempTargetCoord.x, tempTargetCoord.y, _filler.go);
            _incomingSocketsToPlace = 0;
            _outgoingSocketsToPlace = 0;
            _selectionBlocked = false;
            selected.go = null;
            return;
        }
        PauseGame();

    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    // Helper-Methods

    public void PauseGame()
    {
        if (_pausemenu.go.activeSelf)
        {
            if (_currentGIT == GameInputType.DecimalEncoder)
            {
                _inputs.DecimalEncoder.RotatePlayer.performed += RotatePlayer;

                _inputs.DecimalEncoder.MoveCamera.performed += RotateCamera;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            _pausemenu.go.SetActive(false);
        }
        else
        {
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


    public void UpdatePreview()
    {
        if (selected.go)
        {
            if (_previewObject.go)
            {
                Destroy(_previewObject.go);
            }
            _previewObject.go = Instantiate(selected.go);

            _previewObject.go.GetComponent<Renderer>().material = _previewMat;
            int x = _previewObject.go.GetComponent<GridElement>().size.x;
            int y = _previewObject.go.GetComponent<GridElement>().size.y;
            float sizeOffsetX = 1 * x / _previewObject.go.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1 * y / _previewObject.go.GetComponent<Renderer>().bounds.size.y;

            _previewObject.go.transform.localScale = new Vector3(_mainGrid.grid.FieldSize * sizeOffsetX, _mainGrid.grid.FieldSize * sizeOffsetY, 1);
        }
        else
        {
            Destroy(_previewObject.go);
            _previewObject.go = null;
        }

    }

}
