using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Important to work with ScriptableObjectVariables, since these are only loaded, when they are referenced. 
 * All Singleton-Scriptableobjects must be referenced by this script, and this script has to be active in a scene, to use the singleton pattern.
 * Also crucial parts of the scene are connected to their ScriptableObjectsVariables.
 * Must be set onto the Main-Camera of every Scene.
 */ 
public class SceneInitializer : MonoBehaviour
{
    // Rerefencing all Singleton-ScriptableObjects, so they are available in the Scene.
    public SceneTransitionManager gameManager;
    public GridClickHandler clickHandler;
    public AudioManager audioManager;

    // Reference to the main Camera ScriptableObjectVariable.
    public GameObjectSO mainCamera;

    // Reference to the soundeffect Audiosource and it's ScriptableObjectVariable.
    public GameObjectSO audioSourceSFX;
    public GameObject audioSourceGOSFX;

    // Reference to the music Audiosource and it's ScriptableObjectVariable.
    public GameObjectSO audioSourceMusic;
    public GameObject audioSourceGOMusic;

    // Reference to the pause Menu GameObject and it's ScriptableObjectVariable.
    public GameObjectSO pauseMenu;
    public GameObject pauseMenuGO;

    // On Awake, saying the scene is loaded, all Objects are set to their ScriptableObjectVariables.
    private void Awake()
    {
        mainCamera.go = this.gameObject;
        audioSourceSFX.go = audioSourceGOSFX;
        audioSourceMusic.go = audioSourceGOMusic;
        if (pauseMenuGO)
        {

            pauseMenu.go = pauseMenuGO;
        }
        audioManager.InitAudio();
    }

}
