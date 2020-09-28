using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public static GameObject PlaceObject(Grid grid,GameObject obj, int posx, int posy)
    {
        GameObject referenceObj = grid.GetObjFromCoordinate(new Vector2Int(posx, posy));
        if (referenceObj == null || referenceObj.GetComponent<GridElement>().isRemoveable)
        {

            Vector3 pos = grid.GetWorldPosition(posx, posy);
            pos.x += grid.FieldSize / 2;
            pos.y += grid.FieldSize / 2;
            GameObject tempObj = Instantiate(obj, pos, obj.transform.rotation);
            tempObj.transform.localScale = new Vector3(1, 1, 1);
            float sizeOffsetX = 1f / tempObj.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1f / tempObj.GetComponent<Renderer>().bounds.size.y;
            Destroy(referenceObj);

            tempObj.transform.localScale = new Vector3(grid.FieldSize * sizeOffsetX , grid.FieldSize *sizeOffsetY, 1);
            grid.PlaceSelectable(tempObj, new Vector2Int(posx, posy));
            return tempObj;
        }
        return null;
    }
    public static GameObject PlaceObject(Grid grid, GameObject obj, int posx, int posy,GameObject filler)
    {
        GameObject referenceObj = grid.GetObjFromCoordinate(new Vector2Int(posx, posy));
        if (referenceObj == null || referenceObj.GetComponent<GridElement>().isRemoveable)
        {

            Vector3 pos = grid.GetWorldPosition(posx, posy);
            pos.x += grid.FieldSize / 2;
            pos.y += grid.FieldSize / 2;
            GameObject tempObj = Instantiate(obj, pos, obj.transform.rotation);
            tempObj.transform.localScale = new Vector3(1, 1, 1);
            float sizeOffsetX = 1f / tempObj.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1f / tempObj.GetComponent<Renderer>().bounds.size.y;
            if(referenceObj.GetComponent<GridElement>().size.x == 1 && referenceObj.GetComponent<GridElement>().size.x == 2)
            {
                Destroy(referenceObj);
            } else
            {
                Remove(grid, new Vector2Int(posx, posy), filler);
            }


            tempObj.transform.localScale = new Vector3(grid.FieldSize * sizeOffsetX, grid.FieldSize * sizeOffsetY, 1);
            grid.PlaceSelectable(tempObj, new Vector2Int(posx, posy));
            return tempObj;
        }
        return null;
    }
    public static GameObject PlaceMultiCellObject(Grid grid, GameObject obj, int posx, int posy, GameObject filler)
    {
        int x = obj.GetComponent<GridElement>().size.x;
        int y = obj.GetComponent<GridElement>().size.y;
        float sizeOffsetX = 1*x / obj.GetComponent<Renderer>().bounds.size.x;
        float sizeOffsetY = 1*y / obj.GetComponent<Renderer>().bounds.size.y;
        if (posx + x <= grid.Width && posy - (y-1) >= 0)
        {
            if (!Blocked(grid,new Vector2Int(posx,posy),x,y))
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        Remove(grid, new Vector2Int(posx+i, posy-j), filler);
                    }
                }
                Vector3 pos = grid.GetWorldPosition(posx, posy);
                pos.x += x * grid.FieldSize / 2;
                pos.y = ((pos.y + grid.FieldSize) + (pos.y - (grid.FieldSize * (y - 1)))) / 2;
                GameObject tempObj = Instantiate(obj, pos, obj.transform.rotation);
                tempObj.transform.localScale = new Vector3(grid.FieldSize * sizeOffsetX, grid.FieldSize * sizeOffsetY, 1);
                grid.PlaceSelectable(tempObj, new Vector2Int(posx, posy));
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        grid.PlaceSelectable(tempObj, new Vector2Int(posx + i, posy - j));
                    }
                }
                return tempObj;
            }
        }
        return null;
    }
    private static bool Blocked(Grid grid,Vector2Int startingPoint,int x, int y)
    {
        bool blocked = false;
        Vector2Int currectCoord = new Vector2Int();
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                currectCoord.x = startingPoint.x + i;
                currectCoord.y = startingPoint.y - j;
                if (!grid.GetObjFromCoordinate(currectCoord).GetComponent<GridElement>().isRemoveable)
                {
                    blocked = true;
                }
            }
        }
        return blocked;
    }
    public static void Remove(Grid grid,Vector2Int coords, GameObject filler, bool completeRemove =false)
    {
        List<Vector2Int> socketRemovelCoords = new List<Vector2Int>();
        int x = grid.GetObjFromCoordinate(coords).GetComponent<GridElement>().size.x;
        int y = grid.GetObjFromCoordinate(coords).GetComponent<GridElement>().size.y;
        if (x ==1 && y == 1)
        {
            Destroy(grid.GetObjFromCoordinate(coords));
            if (completeRemove)
            {
                PlaceObject(grid, filler, coords.x, coords.y);
            }
        } else 
        {
            foreach (RouteData tempCoord in grid.GetObjFromCoordinate(coords).GetComponent<MultiCellElement>().incomingSockets)
            {
                grid.GetObjFromCoordinate(tempCoord.position).GetComponent<GridElement>().isRemoveable = true;
                Remove(grid, tempCoord.position, filler,true);
            }
            foreach (RouteData tempCoord in grid.GetObjFromCoordinate(coords).GetComponent<MultiCellElement>().outgoingSockets)
            {
                grid.GetObjFromCoordinate(tempCoord.position).GetComponent<GridElement>().isRemoveable = true;
                Remove(grid, tempCoord.position, filler,true);
            }
            Vector2Int startingPoint = FindStartingPoint(grid,coords);
            
            Destroy(grid.GetObjFromCoordinate(coords));
            
            Vector2Int currectCoord = new Vector2Int();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    currectCoord.x = startingPoint.x + i;
                    currectCoord.y = startingPoint.y - j;
                    if(currectCoord != coords || completeRemove)
                    {
                        PlaceObject(grid, filler, currectCoord.x, currectCoord.y);
                    }

                }
            }
        }
    }
    private static Vector2Int FindStartingPoint(Grid grid,Vector2Int coords)
    {
        GameObject tempObj = grid.GetObjFromCoordinate(coords);
        int x = tempObj.GetComponent<GridElement>().size.x;
        int y = tempObj.GetComponent<GridElement>().size.y;
        Vector2Int newCoords = coords;
        Vector2Int tempCoords = coords;
        for (int i = 1; i < x; i++)
        {
            tempCoords = coords;
            tempCoords.x -= i;
            if (tempCoords.x >= 0 && tempCoords.x < grid.Width)
            {
                if (grid.GetObjFromCoordinate(tempCoords) == tempObj)
                {
                    newCoords.x -= 1;
                }
            }
        }
        for (int i = 1; i < y; i++)
        {
            tempCoords = coords;
            tempCoords.y += i;
            if (tempCoords.y >= 0 && tempCoords.y < grid.Height)
            {
                if (grid.GetObjFromCoordinate(tempCoords) == tempObj)
                {
                    newCoords.y += 1;
                }
            }
        }
        return newCoords;
    }
    public static void PlacePreview(Grid grid, GameObject obj, int posx, int posy)
    {

        int x = obj.GetComponent<GridElement>().size.x;
        int y = obj.GetComponent<GridElement>().size.y;
        Vector3 pos = grid.GetWorldPosition(posx, posy);
        pos.x += x * grid.FieldSize / 2;
        pos.y = ((pos.y + grid.FieldSize) + (pos.y - (grid.FieldSize * (y - 1)))) / 2;
        pos.z -= 0.5f;
        obj.transform.position = pos;
    }
    public static bool IsAdjacent(Grid grid, Vector2Int coords, Vector2Int otherCoords)
    {
        Vector2Int startingPoint = FindStartingPoint(grid, otherCoords);
        int sizeX = grid.GetObjFromCoordinate(startingPoint).GetComponent<GridElement>().size.x;
        int sizeY = grid.GetObjFromCoordinate(startingPoint).GetComponent<GridElement>().size.y;
        if((startingPoint.x - coords.x == 1 || coords.x - (startingPoint.x + sizeX) == 0)
            && coords.y <= startingPoint.y && coords.y > startingPoint.y - sizeY)
        {
            return true;
        }
        if ((coords.y - startingPoint.y == 1 || startingPoint.y - sizeY - coords.y == 0)
            && coords.x >= startingPoint.x && coords.x < startingPoint.x + sizeX)
        {
            return true;
        }
        return false;
    }

}
