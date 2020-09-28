using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class to handle scene Transistions. 
 * Holds references to all neccessary Scriptable Objects, that need to be changed when switching scenes.
 * On Scene transition, these Scriptableobject-Variables are then adjusted to enable level-generation in the next-scene.
 */ 
[CreateAssetMenu(menuName = "Manager/SceneTransitionManager")]
public class SceneTransitionManager : SingletonScriptableObject<SceneTransitionManager>
{
    // Holds references to all ScriptableObjectVariables used for Level-Generation on Game and Tutorial-Scenes.
    [SerializeField] private IntSO _variableCount;
    [SerializeField] private IntArraySO _expectedValues;
    [SerializeField] private IntSO _difficulty;
    [SerializeField] private IntSO _outputCount;
    [SerializeField] private IntSO _timeLimit;
    [SerializeField] private IntSO _reward;
    [SerializeField] private IntSO _nextScene;
    [SerializeField] private IntSO _gameMode;
    [SerializeField] private GameObjectSO _audioSourceSFX;

    /** 
     * On transition, set the ScriptableObjectVariables to the values given by the GateBuilderSetup.
     * @param   param   A ScriptableObject that holds information about the scene Transition, like game-mode, reward, difficulty and so on.
     */ 
    public void Transition(GateBuilderSetupSO param)
    {
        // Setting the ScriptableObjectVariables to the corresponding values in param.
        _variableCount.value = param._variableCount;
        _expectedValues.valueArray = param._expectedValues;
        _difficulty.value = param.difficulty;
        _outputCount.value = param.outputCount;
        _timeLimit.value = param.timeLimit;
        _reward.value = param.reward;

        // Calculate the next Scene-index by checking the values in param.
        // Since there are only 5 different cases, it is solved by checking for each case individually.
        // For more cases, a switch statement or hashmap could be used.
        int nextScene = 0;
        if(param.gameType == GameType.GateBuilder)
        {
            nextScene = 1;
            _gameMode.value = 2;
        }
        if(param.gameType == GameType.KV)
        {
            nextScene = 2;

            _gameMode.value = 3;
        }
        if(param.gameType == GameType.Decimal)
        {
            nextScene = 4;

            _gameMode.value = 1;
        }
        if (param.gameType == GameType.ALU)
        {
            nextScene = 3;

            _gameMode.value = 4;
        }
        if(param.gameType == GameType.Intro)
        {
            nextScene = 0;

            _gameMode.value = 0;
        }

        // Set the next-Scene value to the calculated value.
        _nextScene.value = nextScene;

        // If the param indicates a tutorial, load the tutorialscene. If not, load the next-scene directly.
        if (!param.hasTutorial)
        {
            SceneManager.LoadScene(nextScene);
        } else
        {
            SceneManager.LoadScene(5);
        }
    }

    // Loads a new scene.
    public void Transition(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    // Exit the game by quitting the application.
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
