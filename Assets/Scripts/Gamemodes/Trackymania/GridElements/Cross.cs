using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : GridElement
{
    private Vector2Int endpoints = new Vector2Int(1,1);


    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        routes.Add(route);
        Vector2Int result = new Vector2Int(0, 0)
        {
            x = endpoints.x & startCoord.x,
            y = endpoints.y & startCoord.y
        };
        return result;
    }

}
