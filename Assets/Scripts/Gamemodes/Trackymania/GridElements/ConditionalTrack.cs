using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConditionalTrack : Track
{
    [SerializeField] private bool _transistorType;
    [SerializeField] private HashMapSO _charges;
    [SerializeField] private IntSO _variablecount;
    public Charge _actualCharge = Charge.A;
    [SerializeField] private TMP_Text _chargeText;

    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        Vector2Int result = Vector2Int.zero;
        if (_transistorType == _charges.keyValuePairs[_actualCharge.ToString()[0]])
        {
            result = new Vector2Int(endpoints.x & startCoord.x, endpoints.y & startCoord.y);
        }
        routes.Add(route);
        return result;
    }
    public void ChangeCharge(int newCharge)
    { 
        if(newCharge <= _variablecount.value)
        {

            this._actualCharge = (Charge)newCharge;
            _chargeText.text = this._actualCharge.ToString();
        }
    }

}
