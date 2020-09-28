using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/**
 * Class to Set inputhandling and provide button-Methods for the options.
 */ 
public class MenuInputHandler : MonoBehaviour
{
    // Holds a reference to the next scene scriptableObjectVariable, used for scene-transitioning.
    // This is needed to return to the main-menu after replaying the intro, since this would usually be set when reading GameSetupParams.
    [SerializeField] private IntSO _nextScene; 

    // When the Scene is loaded, the inputs are set and the cursor is set free, in case it was locked in the previous Scene.
    public void Awake()
    {
        // Configure Inputhandling
        GridClickHandler.instance.ChangeGIT(GameInputType.Menu);

        // Set Cursorstate to free and visible.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Function for buttons. This loads the tutorial Scene with the intro, which would usually be loaded the first time the game runs.
    public void ReplayIntro()
    {
        _nextScene.value = 0;
        SceneTransitionManager.instance.Transition(5);
    }
}
