using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to be used in a button to skip a stage completely.
public class Skipper : MonoBehaviour
{
    // Reference to the reward, which would be gotten by completing the stage.
    [SerializeField] private IntSO _reward;

    // Set the new gamestate using the reward-value and go back to the main menu.
    public void SkipStage()
    {
        GameStateHandler.SetGameState(_reward.value);
        SceneTransitionManager.instance.Transition(0);
    }
}
