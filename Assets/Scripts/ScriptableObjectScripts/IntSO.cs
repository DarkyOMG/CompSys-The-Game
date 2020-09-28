using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores an Int. Used to share an Int over multiple scenes and scripts.
 */ 
[CreateAssetMenu(fileName = "IntSO", menuName = "ScriptableObjects/IntSO", order = 1)]
public class IntSO : ScriptableObject
{
    public int value;
}
