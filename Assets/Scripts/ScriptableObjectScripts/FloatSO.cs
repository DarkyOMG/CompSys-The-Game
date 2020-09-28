using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores a float. Used to share a float over multiple scenes and scripts.
 */
[CreateAssetMenu(fileName = "FloatSO", menuName = "ScriptableObjects/FloatSO", order = 1)]
public class FloatSO : ScriptableObject
{
    public float value;
    public void SetValue(float newValue)
    {
        value = newValue;
    }

}
