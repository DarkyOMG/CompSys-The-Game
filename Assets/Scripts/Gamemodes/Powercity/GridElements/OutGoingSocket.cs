using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutGoingSocket : MultiSocketObject
{
    public bool isVisitable = false;
    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        throw new System.NotImplementedException();
    }

    public override void VisitMulti(int charge, int route, Vector2Int startCoord, List<RouteData> routeQueue, List<GridElement> resetList = null)
    {
        routes.Add(route);
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x + 1, startCoord.y) });
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x - 1, startCoord.y) });
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x, startCoord.y + 1) });
        routeQueue.Add(new RouteData { charge = charge, route = route, position = new Vector2Int(startCoord.x, startCoord.y - 1) });
    }


}
