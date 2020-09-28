using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores a list of booleanTerms. Used to share a list of boolean Terms over multiple scenes and scripts.
 */
[CreateAssetMenu(fileName = "BooleanTermListSO", menuName = "ScriptableObjects/BooleanTermListSO", order = 1)]
public class BooleanTermListSO : ScriptableObject
{
    public List<KVHandler.BooleanTerm> booleanTerms;
}
