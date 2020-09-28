using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores an Int-Array. Used to share an Int-Array over multiple scenes and scripts.
 */
[CreateAssetMenu(fileName = "IntArraySO", menuName = "ScriptableObjects/IntArraySO", order = 1)]
public class IntArraySO : ScriptableObject
{
    public int[] valueArray;
}
