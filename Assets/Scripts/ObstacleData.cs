using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacleData", menuName = "Grid/ObstacleData")]

public class ObstacleData :  ScriptableObject
{
    // A 10x10 grid flattened into a 1D array
    public bool[] obstacleGrid = new bool[100]; 
}
