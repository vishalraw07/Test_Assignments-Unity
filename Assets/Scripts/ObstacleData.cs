using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacleData", menuName = "Grid/ObstacleData")]

public class ObstacleData :  ScriptableObject
{
    // A 10x10 grid flattened into a 1D array
    public bool[] obstacleGrid; 
    //public bool[] Obstacles;  // Array to hold obstacle data

    private void OnValidate()
    {
        // Ensure the array has exactly 100 elements (10x10 grid)
        if (obstacleGrid == null || obstacleGrid.Length != 100)
        {
            obstacleGrid = new bool[100];
        }
    }
}
