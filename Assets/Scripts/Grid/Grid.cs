using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _fieldSize;
    public float FieldSize { get => _fieldSize; }
    public int Width { get => _width; }
    public int Height { get => _height; }

    private GameObject[,] gridArray;
    private Vector3 _origin;
    
    public Grid(int width, int height, float fieldSize, Vector3 origin)
    {
        this._width = width;
        this._height = height;
        this._fieldSize = fieldSize;
        this._origin = origin;

        gridArray = new GameObject[width, height];
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        if(x>=0 && y>=0 && x <= _width && y <= _height)
        {
            return new Vector3(x, y) * _fieldSize + _origin;
        }
        return new Vector3(-1, -1, -1);
    }

    public Vector2Int GetCoordinate(Vector3 worldPosition)
    {
        Vector2Int result = new Vector2Int();
        result.x = Mathf.FloorToInt((worldPosition.x-_origin.x) / _fieldSize);
        result.y = Mathf.FloorToInt((worldPosition.y-_origin.y) / _fieldSize);
        if(result.x >= 0 && result.y >= 0 && result.x < _width && result.y < _height)
        {
            return result;
        }
        return new Vector2Int(-1, -1);
    }
    public void PlaceSelectable(GameObject selectObj,Vector2Int position)
    {
        
        gridArray[(int)position.x,(int)position.y] = selectObj;
        
    }
    public GameObject GetObjFromCoordinate(Vector2Int coordinates)
    {
        return gridArray[coordinates.x, coordinates.y];
    }

}
