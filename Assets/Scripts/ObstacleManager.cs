using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;

    // Prefab to represent the obstacle (e.g., a red sphere)
    public GameObject obstaclePrefab;

    public Transform gridParent;

    public float spacing = 1.0f; // Space between obstacles
    public float height = 0.5f; // Height of obstacles above the ground

    void Start()
    {
        GenerateObstacles();
    }


    public void GenerateObstacles()
    {
        // clear existing obstacles
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Generate obstacles based on the obstacleData
        for(int i = 0; i < obstacleData.obstacleGrid.Length;i++)
        {

                if(obstacleData.obstacleGrid[i])
                {
                    int x = i % 10;
                    int y = i / 10;

                // Slightly above the grid
                Vector3 position = new Vector3(x * spacing, height, y * spacing);

                GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity, gridParent);
                }
            
        }

    }
}
