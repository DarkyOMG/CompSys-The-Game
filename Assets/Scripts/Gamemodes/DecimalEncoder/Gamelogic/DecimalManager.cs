using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecimalManager : MonoBehaviour
{
    [SerializeField] private GameObjectSO _mainCamera;
    [SerializeField] private GameObjectSO _player;
    [SerializeField] private GameObject _playerGo;
    [SerializeField] private Vector3[] _spawningPoints;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private IntSO _numberToDecode;
    [SerializeField] private IntSO _difficulty;
    private List<GameObject> _guests;
    [SerializeField] private IntSO _reward;
    [SerializeField] private IntSO _gameState;
    [SerializeField] private BaseSetter[] _baseButtons;

    private void Awake()
    {

        _guests = new List<GameObject>();
    }
    private void Start()
    {
        _mainCamera.go = this.gameObject;
        _player.go = _playerGo;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InputHandler.instance.ChangeGIT(GameInputType.DecimalEncoder);
        SpawnCubes();
        
        

    }
    public void HighlightCorrectBase(int value)
    {
        foreach(BaseSetter baseTemp in _baseButtons)
        {
            if(baseTemp.value != value)
            {
                baseTemp.GetComponent<Renderer>().material.color = baseTemp.baseColor;
                baseTemp.highlighted = false;
                baseTemp.isLit = false;
            } else
            {
                baseTemp.GetComponent<Renderer>().material.color = Color.green;
                baseTemp.isLit = true;
            }
        }
    }
    private void SpawnCubes()
    {
        for (int i = 1; i <= 19; i++)
        {
            
            SpawnCube(i, _spawningPoints[i-1]);
        }



    }

    public void SpawnCube(int value, Vector3 position)
    {
        GameObject temp = Instantiate(_cubePrefab, position, Quaternion.identity);
        temp.GetComponent<PickupBlock>().value = value;
    }
    public void RemoveGuest(GameObject guestToRemove)
    {
        _guests.Remove(guestToRemove);
        CheckWin();
    }
    public void AddGuest(GameObject guestToAdd)
    {
        _guests.Add(guestToAdd);

    }
    private void CheckWin()
    {
        if (_guests.Count == 0)
        {

            GameStateHandler.SetGameState(_reward.value);
            StartCoroutine(LoadNewScene());
        }
    }
    public int GetNumber()
    {
        int rInt = 0;
        if (_difficulty.value == 1)
        {
            rInt = (int)Random.Range(1, 100);

        }
        if (_difficulty.value == 2)
        {
            rInt = (int)Random.Range(10, 1000);

        }
        if (_difficulty.value == 3)
        {
            rInt = (int)Random.Range(100, 10000);


        }
        if (_difficulty.value == 4)
        {
            rInt = (int)Random.Range(100, 1000000);

        }
        return rInt;
    }
    public int GetBase()
    {
        int rBaseInt = 0;
        if (_difficulty.value == 1)
        {
            int[] bases = new int[] { 2, 16 };
            rBaseInt = bases[(int)Random.Range(0, 2)];

        }
        if (_difficulty.value == 2)
        {
            int[] bases = new int[] { 2,4, 8, 16 };
            rBaseInt = bases[(int)Random.Range(0, 4)];

        }
        if (_difficulty.value == 3)
        {
            int[] bases = new int[] { 2, 4 , 6, 8, 16, 20 };
            rBaseInt = bases[(int)Random.Range(0, 5)];

        }
        if (_difficulty.value == 4)
        {
            rBaseInt =(int)Random.Range(2, 21);

        }
        return rBaseInt;

    }
    IEnumerator LoadNewScene()
    {
        while (AudioManager.instance._audioSourceSFX.go.GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }
        SceneTransitionManager.instance.Transition(0);
    }
}
    
