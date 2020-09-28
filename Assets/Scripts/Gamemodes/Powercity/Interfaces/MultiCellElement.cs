using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MultiCellElement : MultiSocketObject
{
    public List<RouteData> incomingSockets = new List<RouteData>();
    public List<RouteData> outgoingSockets = new List<RouteData>();
    public List<OutGoingSocket> outgoingSocketObjects;
    public List<int> indexes = new List<int>();
    public int setting=0;
    public int[] resultList;
    public string name="";
    public bool isPresentable = false;
    [SerializeField] private IntSO _gameState;
    [SerializeField] private int _index;

    public bool Locked {get => (GameStateHandler.GetGameState() & (1<<_index-1) )== 0; }

    public override Vector2Int Visit(int route, Vector2Int startCoord)
    {
        return Vector2Int.zero;
    }

    public void AddToList(List<RouteData> listToAddTo,Vector2Int coords)
    {
        int index = listToAddTo.Count;
        RouteData temp = new RouteData
        {
            route = index,
            position = coords
        };
        listToAddTo.Add(temp);
    }
    public override void Reset()
    {
        base.Reset();
        setting = 0;
        indexes = new List<int>();
        for(int i = 0; i < incomingSockets.Count; i++)
        {
            RouteData tempdata= incomingSockets[i];
            tempdata.charge = 0;
        }
        for (int i = 0; i < outgoingSockets.Count; i++)
        {
            RouteData tempdata = outgoingSockets[i];
            tempdata.charge = 0;
        }
    }

    public override void VisitMulti(int charge, int route, Vector2Int startCoord, List<RouteData> routeQueue, List<GridElement> resetList = null)
    {
        routes.Add(route);
        if(outComingSocketCount == 0 && incomingSocketCount == 1)
        {
            setting = charge;
            return;
        }
        if(incomingSocketCount == 0)
        {
            outgoingSocketObjects[0].isVisitable = true;
            outgoingSocketObjects[0].VisitMulti(charge, route, outgoingSockets[0].position, routeQueue);
            outgoingSocketObjects[0].isVisitable = false;
        }else
        if (incomingSockets.Exists(y => y.position == startCoord))
        {
            RouteData temp = incomingSockets.FirstOrDefault(y => y.position == startCoord);

            if (indexes.Contains(temp.route))
            {
                RouteData tempResData = new RouteData
                {
                    charge = -1,
                    position = new Vector2Int(-1, -1)
                };
                routeQueue.Add(tempResData);
            }
            indexes.Add(temp.route);

             if (charge > 0)
            {
                setting = setting | (1 << temp.route);
            }
            temp.charge = charge;
            if (indexes.Count == incomingSocketCount)
            {
                for(int i = 0; i< outComingSocketCount; i++)
                {
                    outgoingSocketObjects[i].isVisitable = true;
                    int chargeInt = (resultList[setting] & (1 << i)) > 0 ? 1 : 0;
                    outgoingSocketObjects[i].VisitMulti(chargeInt, route, outgoingSockets[i].position, routeQueue);
                    outgoingSocketObjects[i].isVisitable = false;
                }
            }
        }
    }
    
}
