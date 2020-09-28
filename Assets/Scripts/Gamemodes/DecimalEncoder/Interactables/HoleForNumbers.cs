using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleForNumbers : Interactable
{
    [SerializeField] private GameObjectSO _selected;
    [SerializeField] private GameObjectSO _resultBoard;
    [SerializeField] private GameObjectSO _uiManager;

    public override void Activate()
    {
        if (_selected.go)
        {

            _resultBoard.go.GetComponent<ResultBoard>().AddValue(_selected.go.GetComponent<Interactable>().value);
            _selected.go = null;
            _uiManager.go.GetComponent<UIManager>().UpdateUI();
        }

    }
}
