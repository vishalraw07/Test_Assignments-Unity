using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Reference to the tile prefeb
    public GameObject tilePrefeb;

    // Size of the grid
    public int gridSize = 10;

    // To hold references to all tiles in the grid by using 2D array
    private GameObject[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        // Generate the grid when game starts
        GenerateGrid();
    }

    // Method To generate the grid
    void GenerateGrid()
    {
        grid = new GameObject[gridSize, gridSize];

        // Loop through each position in the grid
        for (int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; y++)
            {
                GameObject newTile = Instantiate(tilePrefeb, new Vector3(x,0,y), Quaternion.identity);

                // Name the tile for easier identification
                newTile.name = $"Tile_{x}_{y}";

                // Set the tile's grid position in the Tile script
                newTile.GetComponent<Tile>().SetTilePosition(x, y);

                // Store the tile in the grid array
                grid[x,y] = newTile;
            }
        }
            
    }

   
}
