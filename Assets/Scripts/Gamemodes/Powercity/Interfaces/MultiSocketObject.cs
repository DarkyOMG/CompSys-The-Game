using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiSocketObject : GridElement
{
    public abstract void VisitMulti(int charge,int route, Vector2Int startCoord, List<RouteData> routeQueue,List<GridElement> resetList = null);
}
