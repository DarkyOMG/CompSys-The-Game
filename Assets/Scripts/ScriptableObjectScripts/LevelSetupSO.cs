using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Scriptable Object, that stores a Level-Setup. Used to create levels for various game-modes that are either self-defined or have a certain difficulty.
 */

[CreateAssetMenu(fileName = "GateBuilderSetupSO", menuName = "ScriptableObjects/GateBuilderSetupSO", order = 1)]
public class LevelSetupSO : ScriptableObject
{
    public GameType gameType;
    public int _variableCount;
    public int[] _expectedValues;
    public int difficulty;
    public int outputCount;
    public int timeLimit;
    public int reward;
    public bool hasTutorial;
}
