using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileInfoDisplay : MonoBehaviour
{
    // Reference to the UI Text element to display tile information
    public TMP_Text tileInfoText;

    // Reference to the main camera
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Get the main camera from the scene
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast and check if it hits a tile
        if (Physics.Raycast(ray, out hit))
        {
            // Try to get the Tile component from the hit object
            Tile tile = hit.transform.GetComponent<Tile>();

            // If the object has a Tile component, display its position
            if (tile != null)
            {
                Vector2Int tilePos = tile.GetTilePosition();

                tileInfoText.text = $"Tile Position: ({tilePos.x}, {tilePos.y})";
            }
        }

        else
        {
            // If no tile is hit, display a default message
            tileInfoText.text = "No Tile Selected";
        }
    }
}