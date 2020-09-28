using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles the Tutorial Scene by changing input-behaviour and activates the corresponding panels, to show as tutorial.
 */
public class TutorialHandler : MonoBehaviour
{
    // A modifiable list of panel-Objects. This can be changed in the inspector. It holds all different tutorials and intros.
    [SerializeField] private GameObject[] _panels;
    // Reference to the currently chosen Game mode.
    [SerializeField] private IntSO _gameModeInt;

    private void Start()
    {
        // Change the current inputbehaviour to Tutorial-Input-Behaviour.
        GridClickHandler.instance.ChangeGIT(GameInputType.Intro);
        // Activate the panel for the given Gamemode or intro.
        _panels[_gameModeInt.value].SetActive(true);
    }
}
