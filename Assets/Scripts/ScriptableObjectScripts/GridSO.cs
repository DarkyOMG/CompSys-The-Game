using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores a grid-object. Used to share a grid-object over multiple scenes and scripts.
 */
[CreateAssetMenu(fileName = "GridSO", menuName = "ScriptableObjects/GridSO", order = 1)]
public class GridSO : ScriptableObject
{
    public Grid grid;
}
