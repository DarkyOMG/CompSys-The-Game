using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : Interactable
{
    [SerializeField] private GameObjectSO _resBoard;
    public override void Activate()
    {
        _resBoard.go.GetComponent<ResultBoard>().Reset();
    }
}
