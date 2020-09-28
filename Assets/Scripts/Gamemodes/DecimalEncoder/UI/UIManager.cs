using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObjectSO _uiManagerSO;
    [SerializeField] private GameObjectSO _selected;
    [SerializeField] private TMP_Text _currentNumberInInventory;
    [SerializeField] private TMP_Text _startingNumber;
    [SerializeField] private IntSO _currentNumber;
    [SerializeField] private IntSO _currentBase;

    private void Awake()
    {
        _uiManagerSO.go = this.gameObject;
    }

    public void UpdateUI()
    {
        if (!_selected.go)
        {
            _currentNumberInInventory.text = "";
        } else
        {
            PickupBlock tempBlock = _selected.go.GetComponent<PickupBlock>();
            if (tempBlock && tempBlock.value.ToString() != _currentNumberInInventory.text)
            {
                _currentNumberInInventory.text = tempBlock.value.ToString();
            }
        }
        if(_currentNumber.value == 0 || _currentBase.value == 0)
        {
            _startingNumber.text = "";
        } else
        {
                _startingNumber.text = "Current Conversion: "+DecimalEncoder.encode(_currentNumber.value, _currentBase.value) + " (" + _currentBase.value.ToString() + ")";
        }
        
        
        
    }
}
