using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores a GameObject. Used to share a GameObject over multiple scenes and scripts.
 */
[CreateAssetMenu(fileName = "GameObjectSO", menuName = "ScriptableObjects/GameObjectSO", order = 1)]
public class GameObjectSO : ScriptableObject
{
    public GameObject go;
}
