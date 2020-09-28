using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : GridElement, IRotateable
{
    public Vector2Int endpoints = new Vector2Int(0, 1);

    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        Vector2Int result = new Vector2Int(endpoints.x & startCoord.x, endpoints.y & startCoord.y);
        routes.Add(route);
        return result;
    }

    public void Rotate()
    {
        this.gameObject.transform.Rotate(0, 0, 90);
        int temp = endpoints.x;
        this.endpoints.x = endpoints.y;
        this.endpoints.y = temp;
    }
}
