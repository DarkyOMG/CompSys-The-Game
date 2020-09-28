using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores a hashmap. Used to share a hashmap over multiple scenes and scripts.
 */
[CreateAssetMenu(fileName = "HashMapSO", menuName = "ScriptableObjects/HashMapSO", order = 1)]
public class HashMapSO : ScriptableObject
{
    public Dictionary<char, bool> keyValuePairs;
}
