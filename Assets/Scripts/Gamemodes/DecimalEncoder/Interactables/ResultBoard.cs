using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultBoard : MonoBehaviour
{
    public int value;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private GameObjectSO _resBoardSO;

    private void Start()
    {
        _resBoardSO.go = this.gameObject;
    }

    public void AddValue(int incomingValue)
    {
        value += incomingValue;
        _resultText.text = value.ToString();
    }
    public void Reset()
    {
        value = 0;
        _resultText.text = "";
    }
}
