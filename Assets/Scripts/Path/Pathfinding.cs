using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform playerUnit;  // Reference to the player unit
    public ObstacleData obstacleData;  // Reference to the ScriptableObject storing obstacle data
    public int gridSizeX = 10;  // Width of the grid
    public int gridSizeY = 10;  // Height of the grid
    public float nodeSize = 1.0f;  // Size of each grid tile

    private Node[,] grid;  // 2D array representing the grid
    public List<Node> path;  // Public path list to be accessed by PlayerMovement

    void Start()
    {
        CreateGrid();
    }

    // Create the grid based on the obstacle data
    void CreateGrid()
    {
        if (obstacleData == null)
        {
            Debug.LogError("ObstacleData is not assigned in the Pathfinding script.");
            return;
        }

        grid = new Node[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                bool walkable = true;

                // Check if the index is within the bounds of the obstacle grid array
                int index = y * gridSizeX + x;
                if (index >= 0 && index < obstacleData.obstacleGrid.Length)
                {
                    walkable = !obstacleData.obstacleGrid[index]; // Determine if the node is walkable
                }
                else
                {
                    Debug.LogError($"Index {index} is out of bounds of the obstacleData.Obstacles array.");
                }

                grid[x, y] = new Node(walkable, x, y);
            }
        }
    }


    // Get the node from a world position
    Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / nodeSize);
        int y = Mathf.RoundToInt(worldPosition.z / nodeSize);

        // Clamp to ensure the coordinates are within grid bounds
        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return grid[x, y];
    }

    // Get all valid neighboring nodes of a given node
    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // Skip the current node itself

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                // Ensure the neighboring node is within grid bounds
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    // Calculate the path using A* algorithm
    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = GetNodeFromWorldPoint(startPos);
        Node targetNode = GetNodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
    }

    // Retrace the path from the start to the end node
    void RetracePath(Node startNode, Node endNode)
    {
        path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        // Optionally, you can add code here to visualize the path or trigger movement
    }

    // Calculate the distance between two nodes
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
