using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridElement : MonoBehaviour
{
    public bool isRemoveable;
    public Vector2Int size;
    public int incomingSocketCount;
    public int outComingSocketCount;

    public List<int> routes = new List<int>();
    public bool IsVisited(int route)
    {
        return routes.Contains(route);
    }
    public virtual void Reset()
    {
        routes = new List<int>();
    }
    public abstract Vector2Int Visit(int route, Vector2Int startCoord);
    
}
