using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GridResolver
{

    public static int ResolveGrid(Grid grid, List<Tester.StartingPointData> startingPoints, Vector2Int endPoint)
    {
        int result = 0;
        List<GridElement> resetList = new List<GridElement>();
        foreach (Tester.StartingPointData startingPoint in startingPoints)
        {
            resetList.AddRange(ResolveRoute(grid, startingPoint.position, startingPoint.charge));

        }
        if (grid.GetObjFromCoordinate(endPoint).GetComponent<GridElement>().routes.Count != 1)
        {
            result = -1;
        }
        else
        {
            result = grid.GetObjFromCoordinate(endPoint).GetComponent<GridElement>().routes[0];
        }
        foreach (GridElement gridElem in resetList)
        {
            gridElem.Reset();
        }
        return result;
    }
    private static List<GridElement> ResolveRoute(Grid grid, Vector2Int startingPoint, int routeIndex)
    {
        List<Route> queue = new List<Route>();
        List<GridElement> resetList = new List<GridElement>();
        Route temp;
        GridElement tempObj;
        Vector2Int resultTemp;
        queue.Add(new Route(startingPoint, startingPoint));
        while (queue.Any())
        {
            temp = queue[0];
            tempObj = grid.GetObjFromCoordinate(temp.endPoint).GetComponent<GridElement>();
            resetList.Add(tempObj);
            Debug.DrawLine(grid.GetWorldPosition(temp.startPoint.x, temp.startPoint.y) + new Vector3(grid.FieldSize / 2, grid.FieldSize / 2, 0), grid.GetWorldPosition(temp.endPoint.x, temp.endPoint.y) + new Vector3(grid.FieldSize / 2, grid.FieldSize / 2, 0), Color.green, 1f);
            if (!tempObj.IsVisited(routeIndex))
            {
                resultTemp = tempObj.Visit(routeIndex, temp.endPoint - temp.startPoint);
                if (resultTemp.x == 1)
                {
                    if (temp.endPoint - new Vector2Int(1, 0) != temp.startPoint
                    && IsValid(grid, temp.endPoint + resultTemp))
                    {
                        queue.Add(new Route(temp.endPoint, temp.endPoint - new Vector2Int(1, 0)));
                    }
                    if (temp.endPoint + new Vector2Int(1, 0) != temp.startPoint
                    && IsValid(grid, temp.endPoint + resultTemp))
                    {
                        queue.Add(new Route(temp.endPoint, temp.endPoint + new Vector2Int(1, 0)));
                    }
                }
                if (resultTemp.y == 1)
                {
                    if (temp.endPoint - new Vector2Int(0, 1) != temp.startPoint
                    && IsValid(grid, temp.endPoint + resultTemp))
                    {
                        queue.Add(new Route(temp.endPoint, temp.endPoint - new Vector2Int(0, 1)));
                    }
                    if (temp.endPoint + new Vector2Int(0, 1) != temp.startPoint
                    && IsValid(grid, temp.endPoint + resultTemp))
                    {
                        queue.Add(new Route(temp.endPoint, temp.endPoint + new Vector2Int(0, 1)));
                    }
                }
            }
            queue.Remove(temp);
        }
        return resetList;
    }
    private static bool IsValid(Grid grid, Vector2Int coordinate)
    {
        if (coordinate.x < grid.Width && coordinate.y < grid.Width)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private struct Route
    {
        public Vector2Int startPoint;
        public Vector2Int endPoint;
        public Route(Vector2Int startingPoint, Vector2Int endPoint)
        {
            startPoint = startingPoint;
            this.endPoint = endPoint;
        }

    }
}
