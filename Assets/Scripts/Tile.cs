using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // To store the tile's position
    public int xPosition;
    public int yPosition; 

    // Method to Set the tile's Position
    public void SetTilePosition(int x, int y)
    { 
        xPosition = x;
        yPosition = y; 
    }

    // Method to retrieve the tile's position
    public Vector2Int GetTilePosition()
    {
        return new Vector2Int(xPosition, yPosition);
    }
}
