using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public struct RouteData
{
    public int charge;
    public int route;
    public Vector2Int position;
}
public class Plotter : MonoBehaviour
{
    private Grid _grid;
    private int _height = 100;
    private int _width = 100;
    [SerializeField] private GameObjectSO _mainCamera;
    [SerializeField] private GridSO _mainGrid;
    [SerializeField] private GameObjectSO _selected;
    [SerializeField] private GameObjectSO _filler;
    [SerializeField] private Vector2Int[] _startingpoints;
    [SerializeField] private Vector2Int[] _endPoints;
    [SerializeField] private GameObject _startingPointGo;
    [SerializeField] private GameObject _endPointGo;
    [SerializeField] private GameObject _incomingSocket;
    [SerializeField] private GameObject _outGoingSocket;
    [SerializeField] private IntSO _variableCount;
    [SerializeField] private IntSO _outputCount;
    private MultiCellElement[] _outputs;
    private int[] _results;
    [SerializeField] private IntArraySO _expectedResults;
    private Coroutine _placementCoroutine;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _expectedPanel;
    [SerializeField] private GameObject _floatingText;
    [SerializeField] private GameObjectSO _selectionPanel;
    [SerializeField] private GameObject _selectionPanelGO;
    [SerializeField] private IntSO _rewardSO;
    [SerializeField] private IntSO _gameState;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private AudioClip[] _clips;



    private void Awake()
    {
        _selectionPanel.go = _selectionPanelGO;
        _results = new int[(int)Mathf.Pow(2, _variableCount.value)]; 
        _outputs = new MultiCellElement[_outputCount.value];
        _mainCamera.go = this.gameObject;
        _grid = new Grid(100, 100, 5, Vector3.zero);
        InputHandler.instance.ChangeGIT(GameInputType.Plotter);
        InputHandler.instance.floatingText = _floatingText;
        _mainGrid.grid = _grid;
        GameObject empty = Resources.Load<GameObject>("Prefabs/emptyCube");
        _filler.go = empty;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                
                GameObject tempObj = Placer.PlaceObject(_grid, empty, i, j);
               
            }
        }
        int offSetX = _startingPointGo.GetComponent<GridElement>().size.x;
        int offSetY = _startingPointGo.GetComponent<GridElement>().size.y;
        for(int i = 0;i< _variableCount.value; i++)
        {
           
            GameObject tempMCE = Placer.PlaceMultiCellObject(_grid, _startingPointGo, _startingpoints[i].x, _startingpoints[i].y, _filler.go);
            if (tempMCE.GetComponentInChildren<TMP_Text>())
            {
                tempMCE.GetComponentInChildren<TMP_Text>().text = ((Charge)i+1).ToString();
            }
            
            Vector2Int tempVecOutgoingSocket = new Vector2Int(_startingpoints[i].x+offSetX, _startingpoints[i].y);
            GameObject tempObj = Placer.PlaceObject(_grid, _outGoingSocket, _startingpoints[i].x+offSetX, _startingpoints[i].y);

            tempMCE.GetComponent<MultiCellElement>().AddToList(tempMCE.GetComponent<MultiCellElement>().outgoingSockets, tempVecOutgoingSocket);
            tempMCE.GetComponent<MultiCellElement>().outgoingSocketObjects.Add(tempObj.GetComponent<OutGoingSocket>());
        }
        for(int j = 0;j< _outputCount.value; j++)
        {

            GameObject tempMCE = Placer.PlaceMultiCellObject(_grid, _endPointGo, _endPoints[j].x, _endPoints[j].y, _filler.go);
            if (tempMCE.GetComponentInChildren<TMP_Text>())
            {
                tempMCE.GetComponentInChildren<TMP_Text>().text = j.ToString();
            }
            Vector2Int tempVecIncomingSocket = new Vector2Int(_endPoints[j].x -1, _endPoints[j].y);
            GameObject tempObj = Placer.PlaceObject(_grid, _incomingSocket, tempVecIncomingSocket.x, tempVecIncomingSocket.y);
            tempObj.GetComponent<SocketOfGridElement>().coordsToTarget = new Vector2Int(_endPoints[j].x, _endPoints[j].y);
            tempObj.GetComponent<SocketOfGridElement>().targetGo = tempMCE;
            tempMCE.GetComponent<MultiCellElement>().AddToList(tempMCE.GetComponent<MultiCellElement>().incomingSockets, tempVecIncomingSocket);
            _outputs[j] = tempMCE.GetComponent<MultiCellElement>();
        }
    }

    public void Solve()
    {
        if (_resultPanel.activeSelf == true)
        {
            _resultPanel.SetActive(false);
            return;
        }
        List<RouteData> routeQueue = new List<RouteData>();
        bool[][] setResults = new bool[(int)Math.Pow(2, _variableCount.value)][];
        for(int i = 0; i < Math.Pow(2, _variableCount.value); i++)
        {
            setResults[i] = new bool[_outputCount.value];
            List<GridElement> resetList = new List<GridElement>();
            for (int j = 0; j < _variableCount.value; j++)
            {
                RouteData temp = new RouteData { charge = i & (1 << j),route = j, position = _startingpoints[j] };
                routeQueue.Add(temp);
            }
            while (routeQueue.Any())
            {
                RouteData tempData = routeQueue[0];
                if (tempData.charge == -1)
                {
                    break;
                }
                
                MultiSocketObject tempObj = _grid.GetObjFromCoordinate(tempData.position) ? _grid.GetObjFromCoordinate(tempData.position).GetComponent<MultiSocketObject>():null;
                if (tempObj && !tempObj.IsVisited(tempData.route))
                {
                    tempObj.VisitMulti(tempData.charge, tempData.route, tempData.position, routeQueue,resetList);
                    resetList.Add(_grid.GetObjFromCoordinate(tempData.position).GetComponent<GridElement>());
                }
                routeQueue.Remove(tempData);
            }
            int res = 0;
            for (int j = 0; j < _outputCount.value; j++)
            {
                if(_outputs[j].routes.Count != 0)
                {
                    setResults[i][j] = true;
                    if (_outputs[j].setting >0)
                    {
                        res |= (1 << j);
                    }
                }
                else
                {
                    setResults[i][j] = false;
                }
            }
            _results[i] = res;
            foreach (GridElement gridElem in resetList)
            {
                gridElem.Reset();
            }
        }
        UITableFiller.FillTable(_resultPanel, _results,  _outputCount.value, _variableCount.value, _expectedResults.valueArray,setResults);

        _resultPanel.SetActive(true);
        _expectedPanel.SetActive(false);
        if (_results.SequenceEqual(_expectedResults.valueArray))
        {

            GameStateHandler.SetGameState(_rewardSO.value);
            AudioManager.instance.PlaySound(_clips[0]);
            _winPanel.SetActive(true);
        }
        else
        {

            AudioManager.instance.PlaySound(_clips[1]);
        }
    }
    
    public void ShowExpectedResults()
    {
        if (_expectedPanel.activeSelf)
        {
            _expectedPanel.SetActive(false);
            return;
        }
        _resultPanel.SetActive(false);
        UITableFiller.FillTable(_expectedPanel, _expectedResults.valueArray, _outputCount.value, _variableCount.value, _expectedResults.valueArray);
        _expectedPanel.SetActive(true);
    }
    
}
