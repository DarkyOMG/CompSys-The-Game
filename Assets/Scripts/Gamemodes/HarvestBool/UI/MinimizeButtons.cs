using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimizeButtons : MonoBehaviour
{
    [SerializeField] private IntSO _variableCount;
    [SerializeField] private Button[] _buttons;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _variableCount.value; i++)
        {
            _buttons[i * 2].interactable = true;
            _buttons[(i * 2)+1].interactable = true;
        }
    }
    
}
