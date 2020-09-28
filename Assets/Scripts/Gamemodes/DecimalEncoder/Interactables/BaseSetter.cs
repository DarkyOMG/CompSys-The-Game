using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseSetter : Interactable
{
    [SerializeField] private IntSO _baseSO;
    [SerializeField] private GameObjectSO _mainCam;

    public TMP_Text text;

    private void Start()
    {
        text.text = value.ToString();
    }
    public override void Activate()
    {
        _baseSO.value = value;
        _mainCam.go.GetComponent<DecimalManager>().HighlightCorrectBase(value);
    }
    
    
}
