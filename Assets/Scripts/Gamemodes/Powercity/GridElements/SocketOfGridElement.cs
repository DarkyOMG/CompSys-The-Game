using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketOfGridElement : MultiSocketObject
{
    public GameObject targetGo;
    public Vector2Int coordsToTarget;

    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        return Vector2Int.zero;
    }

    public override void VisitMulti(int charge, int route, Vector2Int startCoord, List<RouteData> routeQueue, List<GridElement> resetList = null)
    {
        routes.Add(route);
        if (resetList != null)
        {
            resetList.Add(targetGo.GetComponent<GridElement>());
        }

        targetGo.GetComponent<MultiCellElement>().VisitMulti(charge,route, startCoord,routeQueue);
    }
    public override void Reset()
    {
        base.Reset();
        
    }
}
