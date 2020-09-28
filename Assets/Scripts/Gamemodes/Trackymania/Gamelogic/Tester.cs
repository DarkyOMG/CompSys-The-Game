using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Linq;

public enum Charge {A = 1,B,C,D,E,F,G,H,I};
public class Tester : MonoBehaviour
{
    public Grid grid;
    public Grid inventory;
    [SerializeField] private HashMapSO _charges;
    [SerializeField] private List<StartingPointData> _startingpoints;
    [SerializeField] private Vector2Int _endpoint;
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private int _cellSize;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private IntSO _variableCount;
    [SerializeField] private GameObject _panel;
    [SerializeField] private IntArraySO _expectedResults;
    private int[] _resolveResults;
    [SerializeField] private GameObject _expectedPanel;
    [SerializeField] private GridSO _mainGrid;
    [SerializeField] private GridSO _auxGrid;
    [SerializeField] private GameObject _tutPanel;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private IntSO _rewardSO;
    [SerializeField] private IntSO _gameState;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private AudioClip[] _clips;

    private void Awake()
    {
        UITableFiller.FillTable(_panel, _resolveResults, _variableCount.value, _expectedResults.valueArray, 2);
        UITableFiller.FillTable(_expectedPanel, _expectedResults.valueArray, _variableCount.value, _expectedResults.valueArray, 2);
        inventory = new Grid(1, 1, 20, new Vector3(-30, 0, 0));
        grid = new Grid(_width, _height, _cellSize, _offset);
        _mainGrid.grid = grid;
        _auxGrid.grid = inventory;
        GameObject ground = Resources.Load<GameObject>("Prefabs/ground");
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Placer.PlaceObject(grid, ground, i, j);

            }
        }
        GridClickHandler.instance.ChangeGIT(GameInputType.GateBuilder);
        Placer.PlaceObject(inventory, ground, 0, 0);
        GameObject start = Resources.Load<GameObject>("Prefabs/start");
        GameObject end = Resources.Load<GameObject>("Prefabs/end");
        foreach (StartingPointData startingpoint in _startingpoints)
        {
            GameObject temp = Placer.PlaceObject(grid, start, startingpoint.position.x, startingpoint.position.y);
            temp.GetComponent<StartingPoint>().SetCharge(startingpoint.charge);
        }
        Placer.PlaceObject(grid, end, _endpoint.x, _endpoint.y);
    }


    private void SetCharges(int combination)
    {
        _charges.keyValuePairs = new Dictionary<char, bool>();
        for (int i = 0; i < _variableCount.value; i++)
        {
            _charges.keyValuePairs.Add(((Charge)i+1).ToString()[0], (combination & (1 << i)) != 0);
        }
    }

    

        
    
    public void Resolve()
    {
        _resolveResults = new int[Convert.ToInt32(Math.Pow(2, _variableCount.value))];
        for(int i = 0; i < Math.Pow(2, _variableCount.value); i++)
        {
            SetCharges(i);
            int output = GridResolver.ResolveGrid(grid, _startingpoints, _endpoint);
            _resolveResults[i] = output;
        }
        if (_resolveResults.SequenceEqual(_expectedResults.valueArray))
        {

            GameStateHandler.SetGameState(_rewardSO.value);
            AudioManager.instance.PlaySound(_clips[0]);
            _winPanel.SetActive(true);

        } else
        {

            AudioManager.instance.PlaySound(_clips[1]);
        }

        UITableFiller.FillTable(_panel,_resolveResults,_variableCount.value,_expectedResults.valueArray,2);
    }

    public Grid GetGrid()
    {
        return grid;
    }

    public Grid GetInventory()
    {
        return inventory;
    }

    [Serializable]
    public struct StartingPointData
    {
        public Vector2Int position;
        public int charge;
    }
}
