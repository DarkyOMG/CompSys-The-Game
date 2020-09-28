using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : GridElement
{


    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        routes.Add(route);
        return new Vector2Int(0, 0);
    }
}
