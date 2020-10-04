using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/**
 * Class to handle the Tutorial-Slide-progression for a given panel, aswell as the scene-transition into the game-scene.
 */ 
public class TutorialPanelHandler : MonoBehaviour
{
    // Panels to iterate through. These can be dragged-and-dropped in the inspector.
    [SerializeField] private GameObject[] _panels;
    // If panels have corresponding videoclips, drag them in in the inspector.
    [SerializeField] private VideoClip[] _videoClips;
    // Contains a reference to the videoplayer, that is used to play videos on rendertextures.
    [SerializeField] private VideoPlayer _vidPlayer;

    // A reference to the Index of the next Scene.
    [SerializeField] private IntSO _nextScene;

    // Index of the current panel-slides. Starting at 0.
    private int _index = 0;

    // Activate the first Panel, when activated.
    private void Start()
    {
        _panels[0].SetActive(true);
    }

    // Activates the next panel and adjusts videoclips on the videoplayer and index.
    // If the end of the slides have been reached, switch to the next scene.
    public void NextPanel()
    {
        _index++;
        if(_index < _panels.Length)
        {
            _panels[_index - 1].SetActive(false);
            _panels[_index].SetActive(true);
            if (_videoClips[_index])
            {
                _vidPlayer.Stop();
                _vidPlayer.clip = _videoClips[_index];
                _vidPlayer.Play();
            }
        } else
        {
            SceneTransitionManager.instance.Transition(_nextScene.value);
        }
    }

    // Used to go back a panel by decreasing the index, deactivating the current panel and activating the previous one.
    // Also adjusts the current videoclip which is played by the videoplayer.
    public void PreviousPanel()
    {
        _index--;
        if(_index < 0)
        {
            _index = 0;
        }
        _panels[_index + 1].SetActive(false);
        _panels[_index].SetActive(true);
        if (_videoClips[_index])
        {
            _vidPlayer.Stop();
            _vidPlayer.clip = _videoClips[_index];
            _vidPlayer.Play();
        }
    }
}
