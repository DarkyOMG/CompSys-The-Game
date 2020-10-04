using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Helper Class to place objects onto a given grid. 
 * This is needed, since only monobehaviours can create Objects in the scene and the grid-class by itsself doesn't inherit the monobehaviour.
 * Resizes the given objects to fit the grids cellsize and calculates which cells are needed to place a multi-cell-element.
 * Also resolves conflicts while placing elements on the grid.
 */ 
public class Placer : MonoBehaviour
{
    /*
     * Most simple implementation of object placement. Used in Trackymania.
     * Places an Object on given coordinates on a given grid, by destroying the object on the given coordinates and replacing it.
     * @param   grid    The grid in which to place the object.
     * @param   obj     The object to be placed in the grid.
     * @param   posx    x-Coordinate of the wished destination in the grid.
     * @param   posy    y-Coordinate of the wished destination in the grid.
     * @return          Returns a reference to the newly created object in the grid.
     */
    public static GameObject PlaceObject(Grid grid,GameObject obj, int posx, int posy)
    {
        // Get a reference to the current object in the grid on the specified coordinates.
        GameObject referenceObj = grid.GetObjFromCoordinate(new Vector2Int(posx, posy));

        // If there is no object, or the object on the coodinates is removable, remove it and replace it.
        if (referenceObj == null || referenceObj.GetComponent<GridElement>().isRemoveable)
        {
            // Get the world position of the desired location. 
            Vector3 pos = grid.GetWorldPosition(posx, posy);

            // Add offset to place the new object in the middle of the bound of the given cell.
            pos.x += grid.FieldSize / 2;
            pos.y += grid.FieldSize / 2;

            // Create a copy of the given object to place it in the grid.
            GameObject tempObj = Instantiate(obj, pos, obj.transform.rotation);
            // Reset the scale of the object to 1, so we can adjust it to the correct size later.
            tempObj.transform.localScale = new Vector3(1, 1, 1);

            // Calculate the needed scale, by deviding 1 with the bounds of the object in x and y. 
            // This tells us the needed scaling to reach y and x bounds of 1. This can then be multiplied by the cellsize.
            float sizeOffsetX = 1f / tempObj.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1f / tempObj.GetComponent<Renderer>().bounds.size.y;

            // Delete the object on the grid-coordinates.
            Destroy(referenceObj);

            // After calculating the sizeoffset, multiply the offset with the cellsize to get the correct size of the object.
            tempObj.transform.localScale = new Vector3(grid.FieldSize * sizeOffsetX , grid.FieldSize *sizeOffsetY, 1);

            // At last, add the object to the grids internal datastructure.
            grid.PlaceSelectable(tempObj, new Vector2Int(posx, posy));

            // Return the newly created object as reference.
            return tempObj;
        }

        // If the placement was not successfull, return null to indicate that no object was placed.
        return null;
    }


    /*
     * Placement of single-cell-elements that could lead to conflicts and needs a filling element.
     * Places an element on the given coordinates in the given.
     * In case of a conflict with a multi-cell-element fills the rest of the bigger element with filler-elements.
     * @param   grid    The grid in which to place the object.
     * @param   obj     The object to be placed in the grid.
     * @param   posx    x-Coordinate of the wished destination in the grid.
     * @param   posy    y-Coordinate of the wished destination in the grid.
     * @param   filler  object to fill freed cells, in case a multicell-element needs to be removed.
     * @return          Returns a reference to the newly created object in the grid. 
     */
    public static GameObject PlaceObject(Grid grid, GameObject obj, int posx, int posy,GameObject filler)
    {
        // Get a reference to the current object in the grid on the specified coordinates.
        GameObject referenceObj = grid.GetObjFromCoordinate(new Vector2Int(posx, posy));

        // If there is no object, or the object on the coodinates is removable, remove it and replace it.
        if (referenceObj == null || referenceObj.GetComponent<GridElement>().isRemoveable)
        {
            // Get the world position of the desired location. 
            Vector3 pos = grid.GetWorldPosition(posx, posy);

            // Add offset to place the new object in the middle of the bound of the given cell.
            pos.x += grid.FieldSize / 2;
            pos.y += grid.FieldSize / 2;

            // Create a copy of the given object to place it in the grid.
            GameObject tempObj = Instantiate(obj, pos, obj.transform.rotation);

            // Reset the scale of the object to 1, so we can adjust it to the correct size later.
            tempObj.transform.localScale = new Vector3(1, 1, 1);

            // Calculate the needed scale, by deviding 1 with the bounds of the object in x and y. 
            // This tells us the needed scaling to reach y and x bounds of 1. This can then be multiplied by the cellsize.
            float sizeOffsetX = 1f / tempObj.GetComponent<Renderer>().bounds.size.x;
            float sizeOffsetY = 1f / tempObj.GetComponent<Renderer>().bounds.size.y;

            // If the obect on the given coordinates is a single-cell element, just remove it.
            if(referenceObj.GetComponent<GridElement>().size.x == 1 && referenceObj.GetComponent<GridElement>().size.x == 2)
            {
                Destroy(referenceObj);
            }
            // If the object is a multicell object, invoke the remove-method to replace emptied places with filler-objects.
            else
            {
                Remove(grid, new Vector2Int(posx, posy), filler);
            }


            // After calculating the sizeoffset, multiply the offset with the cellsize to get the correct size of the object.
            tempObj.transform.localScale = new Vector3(grid.FieldSize * sizeOffsetX, grid.FieldSize * sizeOffsetY, 1);

            // At last, add the object to the grids internal datastructure.
            grid.PlaceSelectable(tempObj, new Vector2Int(posx, posy));

            // Return the newly created object as reference.
            return tempObj;
        }
        // If the placement was not successfull, return null to indicate that no object was placed.
        return null;
    }

    /*
     * 
     * 
     */ 
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
    private static bool Blocked(Grid grid, Vector2Int startingPoint, int x, int y)
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
}
