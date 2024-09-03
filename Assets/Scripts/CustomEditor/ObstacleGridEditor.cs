using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleGridEditor : Editor
{
   public override void OnInspectorGUI()
   {
        ObstacleData obstacleData = (ObstacleData)target;

        GUILayout.Label("Obstacle Grid", EditorStyles.boldLabel);

        // Create a 10x10 grid of toggleable buttons
        for(int y = 0; y < 10; y++)
        {
            GUILayout.BeginHorizontal();

            for(int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;

                obstacleData.obstacleGrid[index] = GUILayout.Toggle(obstacleData.obstacleGrid[index], "");
            }

            GUILayout.EndHorizontal();
        }

        if(GUI.changed)
        {
            EditorUtility.SetDirty(obstacleData);
        }

   }
}
