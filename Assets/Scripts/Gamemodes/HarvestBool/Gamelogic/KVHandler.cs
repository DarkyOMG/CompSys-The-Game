using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;

public class KVHandler : MonoBehaviour
{
    [SerializeField] private IntSO _variableCount;
    [SerializeField] private IntArraySO _expectedResults;
    private Grid _grid;
    private Grid _inventory;
    [SerializeField] private GameObject _expectedPanel;
    [SerializeField] private HashMapSO _charges;
    private int _width = 1;
    private int _height = 1;
    private int _currentSelectionPositive = 0;
    private int _currentSelectionNegative = 0;
    [SerializeField] private TMP_Text _booleanExpression;
    public TMP_FontAsset normalFont, otherFont;
    [SerializeField] private GameObject _stageOnePanel, _stageTwoPanel;
    [SerializeField] private BooleanTermListSO _booleanTerms;
    private bool _stageOneFinished = false;
    [SerializeField] private GridSO _mainGrid;

    [SerializeField] private GameObject _tutPanel;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private Vector3 _gridOffset;
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private IntSO _rewardSO; 
    [SerializeField] private IntSO _gameState;
    [SerializeField] private IntSO _difficutly;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _winPanel;







    // Start is called before the first frame update
    void Awake()
    {

        SetParams();
        UITableFiller.FillTable(_expectedPanel, _expectedResults.valueArray, _variableCount.value, _expectedResults.valueArray);
        int cellCount = Convert.ToInt32(Math.Pow(2, _variableCount.value));

        while(_width*_height != cellCount)
        {
            if (_width == _height)
            {
                _width *= 2;
            } else
            {
                _height *= 2;
            }
        }


        _charges.keyValuePairs = new Dictionary<char, bool>();
        _charges.keyValuePairs['A'] = true;
        _charges.keyValuePairs['B'] = true;
        _charges.keyValuePairs['C'] = true;
        _charges.keyValuePairs['D'] = true;
        _charges.keyValuePairs['E'] = true;
        _grid = new Grid(_width, _height, 12, _gridOffset);
        _mainGrid.grid = _grid;

        GridClickHandler.instance.ChangeGIT(GameInputType.KV);
        Dictionary<char, bool> temp = new Dictionary<char, bool>();
        GameObject empty = Resources.Load<GameObject>("Prefabs/empty");


        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                temp = PrepareCharge(i, j);
                GameObject tempObj = Placer.PlaceObject(_grid, empty, i, j);
                tempObj.GetComponent<KVelement>().charge = ChargeCombinationToInt(temp);
                
                tempObj.GetComponent<KVelement>().text.text = ChargeCombinationToInt(temp).ToString();
            }
        }
        DrawIndicators();

    }

    private void DrawIndicators()
    {
        if(_variableCount.value > 1)
        {
            GameObject tempIndicatorObj = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity);
            tempIndicatorObj.transform.localScale = new Vector3(_grid.FieldSize, (_height / 2) * _grid.FieldSize, 1);
            tempIndicatorObj.transform.position = _gridOffset - new Vector3(_grid.FieldSize/2, 0f, 0f) + new Vector3(0f, _height / 2 * _grid.FieldSize / 2, 0f);
            tempIndicatorObj.GetComponent<Renderer>().material.color = Color.red;
            tempIndicatorObj.GetComponentInChildren<TMP_Text>().text = "A";
        }
        if(_variableCount.value >= 2)
        {
            GameObject tempIndicatorObj = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity);
            tempIndicatorObj.transform.localScale = new Vector3((_width / 2) * _grid.FieldSize, _grid.FieldSize, 1);
            tempIndicatorObj.transform.position = _gridOffset + new Vector3(0f, _height*_grid.FieldSize+(_grid.FieldSize/2), 0f) + new Vector3(_width/2*_grid.FieldSize/2, 0f, 0f);
            tempIndicatorObj.GetComponent<Renderer>().material.color = Color.blue;
            tempIndicatorObj.GetComponentInChildren<TMP_Text>().text = "B";
        }
        if (_variableCount.value >= 3)
        {
            GameObject tempIndicatorObj = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity);
            tempIndicatorObj.transform.localScale = new Vector3((_width / 2) * _grid.FieldSize, _grid.FieldSize, 1);
            tempIndicatorObj.transform.position = _gridOffset - new Vector3(0f, _grid.FieldSize/2, 0f) + new Vector3((_width / 2 * _grid.FieldSize / 2)+_grid.FieldSize, 0f, 0f);
            tempIndicatorObj.GetComponent<Renderer>().material.color = Color.green;
            tempIndicatorObj.GetComponentInChildren<TMP_Text>().text = "C";
        }
        if (_variableCount.value >= 4)
        {
            GameObject tempIndicatorObj = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity);
            tempIndicatorObj.transform.localScale = new Vector3(_grid.FieldSize, (_height / 2) * _grid.FieldSize, 1);
            tempIndicatorObj.transform.position = _gridOffset + new Vector3(_grid.FieldSize*_width+_grid.FieldSize/2, 0f, 0f) + new Vector3(0f, (_height / 2 * _grid.FieldSize / 2) + _grid.FieldSize, 0f);
            tempIndicatorObj.GetComponent<Renderer>().material.color = Color.yellow;
            tempIndicatorObj.GetComponentInChildren<TMP_Text>().text = "D";
        }
        if (_variableCount.value >= 5)
        {
            GameObject tempIndicatorObj = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity);
            tempIndicatorObj.transform.localScale = new Vector3(((_width / 2) * _grid.FieldSize)/2, _grid.FieldSize/2, 1);
            tempIndicatorObj.transform.position = _gridOffset + new Vector3(0f, (_height * _grid.FieldSize + (_grid.FieldSize / 2))+_grid.FieldSize/4*3, 0f) + new Vector3(_width / 2 * _grid.FieldSize / 2, 0f, 0f);
            tempIndicatorObj.GetComponent<Renderer>().material.color = Color.cyan;
            tempIndicatorObj.GetComponentInChildren<TMP_Text>().text = "E";

            tempIndicatorObj = Instantiate(_indicatorPrefab, Vector3.zero, Quaternion.identity);
            tempIndicatorObj.transform.localScale = new Vector3(((_width / 2) * _grid.FieldSize) / 2, _grid.FieldSize / 2, 1);
            tempIndicatorObj.transform.position = _gridOffset + new Vector3(0f, (_height * _grid.FieldSize + (_grid.FieldSize / 2)) + _grid.FieldSize/4*3, 0f) + new Vector3(_width / 2 * _grid.FieldSize / 2+(_width/2*_grid.FieldSize), 0f, 0f);
            tempIndicatorObj.GetComponent<Renderer>().material.color = Color.cyan;
            tempIndicatorObj.GetComponentInChildren<TMP_Text>().text = "E";
        }
    }
    public void Highlight(int intToAdd)
    {
        ResetBoard();

        if (intToAdd > 0)
        {
            if ((intToAdd & _currentSelectionPositive) == 0)
            {
                _currentSelectionPositive += intToAdd;
            }
            else
            {
                _currentSelectionPositive -= intToAdd;
            }
        } else
        {
            if ((intToAdd*-1 & _currentSelectionNegative) == 0)
            {
                _currentSelectionNegative += intToAdd*-1;
            }
            else
            {
                _currentSelectionNegative -= intToAdd*-1;
            }
        }


        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GameObject tempHighlight = _grid.GetObjFromCoordinate(new Vector2Int(i, j));
                KVelement currentElement = tempHighlight.GetComponent<KVelement>();
                if (currentElement != null)
                {
                    currentElement.Highlight(_currentSelectionPositive,_currentSelectionNegative);
                }
            }
        }
        SetText();
    }
    private void SetText()
    {
        string temp = "";
        bool firstSet = false;
        for (int i = 0; i < _variableCount.value; i++)
        {
            if ((_currentSelectionPositive & (1 << i)) != 0)
            {

                temp += ((Charge)i+1).ToString()[0];
                firstSet = true;
            }
            if ((_currentSelectionNegative & (1 << i)) != 0)
            {

                temp += "¬" + ((Charge)i+1).ToString()[0];
                firstSet = true;
            }
        }
        _booleanExpression.text = temp;
    }

    public void Solve()
    {

        if (!_stageOneFinished)
        {
            bool result = true;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    KVelement temp = _grid.GetObjFromCoordinate(new Vector2Int(i, j)).GetComponent<KVelement>();
                    if (temp != null)
                    {
                        if (_expectedResults.valueArray[temp.charge] != temp.setting)
                        {
                            result = false;
                        }
                    }
                }
            }
            if (result)
            {
                _booleanTerms.booleanTerms = new List<BooleanTerm>();
                UITableFiller.FillTable(_stageOnePanel, _booleanTerms.booleanTerms);
                _stageTwoPanel.SetActive(true);
                _stageOneFinished = true;
                ChangeGUI();
                GridClickHandler.instance.ChangeGIT(GameInputType.None);
            }
        } else
        {
            if (IsDMF())
            {
                GameStateHandler.SetGameState(_rewardSO.value);
                AudioManager.instance.PlaySound(_clips[0]);
                _winPanel.SetActive(true);
            } else
            {

                AudioManager.instance.PlaySound(_clips[1]);
            }
        }
        
    }
    private bool IsDMF()
    {
        ResetBoard();
        int expectedHits = _expectedResults.valueArray.Sum();
        int hits = 0;
        int literals = 0;
        bool foundOs = false;
        int tempHits = 0;

        foreach(BooleanTerm boolTerm in _booleanTerms.booleanTerms)
        {
            literals += QuineMccluskeySolver.CountOnes(QuineMccluskeySolver.ConvertToTerm(boolTerm.charges.x, _variableCount.value));
            literals += QuineMccluskeySolver.CountOnes(QuineMccluskeySolver.ConvertToTerm(boolTerm.charges.y, _variableCount.value));
            tempHits = GetHits(boolTerm);
            if(tempHits > 0)
            {
                hits += tempHits;
            } else
            {
                foundOs = true;
            }
        }

        if(!foundOs && hits == expectedHits && literals <= QuineMccluskeySolver.Solve(_expectedResults.valueArray, _variableCount.value))
        {
            return true;
        } else
        {
            return false;
        }
    }
    private int GetHits(BooleanTerm boolTerm)
    {
        int hits = 0;
        bool foundOs = false;
        for (int i = 0; i < _width; i++)
        {
            for(int j = 0; j < _height; j++)
            {
                KVelement temp = _grid.GetObjFromCoordinate(new Vector2Int(i, j)).GetComponent<KVelement>();
                if (temp)
                {
                    if (!temp.highlighted)
                    {
                        if (temp.Highlight(boolTerm.charges.x, boolTerm.charges.y) == 1)
                        {
                            hits++;
                        }
                        if (temp.Highlight(boolTerm.charges.x, boolTerm.charges.y) == 0)
                        {
                            foundOs = true;
                        }
                    }
                }
            }
        }
        if (!foundOs)
        {
            return hits;
        } else
        {
            return -1;
        }

    }
    public void ChangeGUI()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                KVelement temp = _grid.GetObjFromCoordinate(new Vector2Int(i, j)).GetComponent<KVelement>();
                if (temp != null)
                {
                    if (!GUIManager.guiState)
                    {

                        temp.text.text = temp.setting.ToString();

                    } else
                    {


                        temp.text.text = temp.charge.ToString();

                    }

                }
            }
        }
        if (GUIManager.guiState)
        {
            GUIManager.guiState = false;
        } else
        {
            GUIManager.guiState = true;
        }

    }
    private string ChargeToString(Dictionary<char,bool> charge)
    {
        string res = "";
        if (charge.ContainsKey('A') && charge['A'])
        {
            res += 1;
        } else
        {
            res += 0;
        }
        if (charge.ContainsKey('B') && charge['B'])
        {
            res += 1;
        }
        else
        {
            res += 0;
        }
        if (charge.ContainsKey('C') && charge['C'])
        {
            res += 1;
        }
        else
        {
            res += 0;
        }
        if (charge.ContainsKey('D') && charge['D'])
        {
            res += 1;
        }
        else
        {
            res += 0;
        }
        if (charge.ContainsKey('E') && charge['E'])
        {
            res += 1;
        }
        else
        {
            res += 0;
        }
        return res;
    }
    public void AddExpressionToResult()
    {

        if(_currentSelectionNegative != 0 || _currentSelectionPositive != 0)
        {
            BooleanTerm temp = new BooleanTerm();
            temp.charges = new Vector2Int(_currentSelectionPositive, _currentSelectionNegative);
            temp.expression = _booleanExpression.text;
            _booleanTerms.booleanTerms.Add(temp);
            UITableFiller.FillTable(_stageOnePanel, _booleanTerms.booleanTerms);
            _currentSelectionPositive = 0;
            _currentSelectionNegative = 0;
            SetText();
        }

    }

    private Dictionary<char,bool> PrepareCharge(int x, int y)
    {
        Dictionary<char, bool> result = new Dictionary<char, bool>();
        result['A'] = y < _height / 2;
        result['B'] = x < _width / 2;
        result['C'] = x >= _width / 2 - _width / 4 && x < _width / 2 + _width / 4;
        result['D'] = y >= _height / 2 - _height / 4 && y < _height / 2 + _height / 4;
        result['E'] = x >= _width / 2 - _width / 4 - _width / 8 && x < _width / 2 - _width / 4 + _width / 8 || x < _width / 2 + _width / 4 + _width / 8 && x >= _width / 2 + _width / 4 - _width / 8;
        if (_variableCount.value == 1)
        {
            result['A'] = x < _width / 2;
        }
        return result;
    }


    private int ChargeCombinationToInt(Dictionary<char,bool> charges)
    {
        int result = 0;
        for (int i = 0; i < _variableCount.value; i++)
        {
            if(charges[((Charge)i+1).ToString()[0]])
            {
                result |= 1 << i;
            }
            
        }

        return result;
    }

    public Grid GetGrid()
    {
        return _grid;
    }

    public Grid GetInventory()
    {
        return null;
    }

    private void ResetBoard()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                KVelement temp = _grid.GetObjFromCoordinate(new Vector2Int(i, j)).GetComponent<KVelement>();
                if (temp)
                {
                    temp.ResetHighlight();
                }
                
            }
        }
    }
    private void SetParams()
    {
        _variableCount.value = _difficutly.value + 1;
        _expectedResults.valueArray = CalculateRandomResults();
    }
    private int[] CalculateRandomResults()
    {
        int[] result = new int[(int)Math.Pow(2, _variableCount.value)];
        while(result.All(y=>y==1)|| result.All(y => y == 0))
        {
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (int)UnityEngine.Random.Range(0, 2);
            }
        }
        return result;
    }

    [Serializable]
    public struct BooleanTerm
    {
        public Vector2Int charges;
        public string expression;
    }
}
