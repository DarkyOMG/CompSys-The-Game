using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Abstract class for Scriptableobjects, that should be singletons. Should only be used for Managers or similar, that need to be singletons.
 */ 
public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;
    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            }
            return _instance;
        }
    }
    
}
