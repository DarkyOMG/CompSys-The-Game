using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MultiSocketObject
{
    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        return new Vector2Int();
    }

    public override void VisitMulti(int charge, int route, Vector2Int startCoord, List<RouteData> routeQueue, List<GridElement> resetList = null)
    {
        routes.Add(route);
        if (routes.Count > 1)
        {
            routeQueue.Add(new RouteData { charge = -1, route = route, position = new Vector2Int(-1, -1) });
            return;
        }
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x + 1, startCoord.y) });
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x - 1, startCoord.y) });
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x,  startCoord.y+1) });
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x, startCoord.y-1) });
    }
}
