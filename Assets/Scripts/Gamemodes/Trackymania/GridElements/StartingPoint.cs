using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartingPoint : GridElement
{
    private int _charge;
    public int Charge { get => _charge; set => _charge = value; }
    [SerializeField] private TMP_Text _chargeText;



    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        routes.Add(route);
        return new Vector2Int(1, 1);
    }
    public void SetCharge(int charge)
    {
        this._charge = charge;
        _chargeText.text = this._charge.ToString();
    }

}
