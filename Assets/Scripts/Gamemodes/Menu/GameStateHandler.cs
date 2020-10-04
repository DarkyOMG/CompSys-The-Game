using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Class to handle all interactions regarding the game-state. 
 * Used to check and alter the current game state.
 * Saves the current game state to the playerPrefs.
 * Also unlocks GameObjects, like buttons for levels and prefabs, depending on the current gamestate.
 */
public class GameStateHandler : MonoBehaviour
{
    // List of all Level-Buttons in the main menu.
    [SerializeField] private Button[] _ButtonList;
    // Reference to a UI panel, that will be shown when the game-state is "won".
    [SerializeField] private GameObject _winPanel;
    // Reference to the main menu panel to deactivate it when the win-panel is shown.
    [SerializeField] private GameObject _mainMenuPanel;

    // Starting state. This unlocks the first level, aswell as all basic prefabs.
    private int _startingstate = 65537;
    // Key for finding the current gamestate in the playerprefs.
    private static string _gameStatePrefsKey = "GameState";
    // Start is called before the first frame update
    void Start()
    {
        // On start, check if the last used bit in the Gamestate-Int is set thus indicating a won game. If so, show the win-panel.
        if (PlayerPrefs.GetInt(_gameStatePrefsKey) >= 2097151)
        {
            _mainMenuPanel.SetActive(false);
            _winPanel.SetActive(true);
        }

        // Check if there is a saved game-state. If not, load the starting state into the playerprefs and show the intro.
        if (!PlayerPrefs.HasKey(_gameStatePrefsKey))
        {
            PlayerPrefs.SetInt(_gameStatePrefsKey, _startingstate);
            SceneTransitionManager.instance.Transition(5);
        }

        // Unlock all buttons and prefabs that are allready unlocked according to the game state.
        // Levels are coded into the gamestate int on the first 16 bits. 1 Bit for each level. 
        // Each game-mode takes 4 bits of the gamestate int. Numberpush takes the first 4 bits, trackymania the second 4 and so on.
        // After the first 16 bits, the prefabs for Powercity are coded into the gamestate. These are the Bits 17 - 21.
        for (int i = 0; i < _ButtonList.Length; i++)
        {
            if ((GetGameState() & (1 << i) )!= 0)
            {
                _ButtonList[i].interactable = true;
            }else
            {
                _ButtonList[i].interactable = false;
            }
        }
    }

    /*
     * Alter the current Game state by adding (bitwise-or) a reward int to the curren gamestate int.
     */ 
    public static void SetGameState(int reward)
    {
        int newVal = PlayerPrefs.GetInt(_gameStatePrefsKey) | reward;
        PlayerPrefs.SetInt(_gameStatePrefsKey, newVal);
    }
    /*
     * Get the current game state int from the playerPrefs.
     */
    public static int GetGameState()
    {
        return PlayerPrefs.GetInt(_gameStatePrefsKey);
    }

    /*
     * Reset the game by deleting all saved data.
     */ 
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
