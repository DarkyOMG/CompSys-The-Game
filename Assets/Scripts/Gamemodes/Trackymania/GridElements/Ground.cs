using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : GridElement
{

    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        return Vector2Int.zero;
    }
    
}
