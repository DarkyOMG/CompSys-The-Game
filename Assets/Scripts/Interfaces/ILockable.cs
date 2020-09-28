using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Interface to enable unlocking of Prefabs and GameObjects.
 * Used to enable GameObjects and Prefabs depending on the game-state.
 */ 
interface ILockable
{
    void Unlock();
}
