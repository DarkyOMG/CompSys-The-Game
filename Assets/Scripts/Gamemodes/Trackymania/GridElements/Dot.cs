using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : GridElement
{
    private Vector2Int endpoints = new Vector2Int(1, 1);
    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        routes.Add(route);
        return new Vector2Int(1, 1);
    }
}
