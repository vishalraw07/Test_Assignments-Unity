using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    public float moveSpeed = 3f;         // Speed of the enemy's movement
    public Transform playerTransform;    // Reference to the player's Transform
    private Pathfinding pathfinding;     // Reference to the Pathfinding component
    private bool isMoving;               // Flag to check if the enemy is currently moving

    void Start()
    {
        // Initialize pathfinding
        pathfinding = GetComponent<Pathfinding>();

        // Check if pathfinding is assigned
        if (pathfinding == null)
        {
            Debug.LogError("Pathfinding component is not assigned or not found.");
        }

        // Check if playerTransform is assigned
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned in EnemyAI.");
        }
    }

    void Update()
    {
        // Only move towards the player if the enemy is not currently moving
        if (!isMoving)
        {
            MoveTowardsPlayer(playerTransform.position);
        }
    }

    public void MoveTowardsPlayer(Vector3 playerPosition)
    {
        if (pathfinding == null) return;

        // Find path to one of the adjacent tiles around the player
        List<Vector3> adjacentPositions = GetAdjacentPositions(playerPosition);

        foreach (var position in adjacentPositions)
        {
            pathfinding.FindPath(transform.position, position);
            if (pathfinding.path != null && pathfinding.path.Count > 0)
            {
                StartCoroutine(MoveAlongPath(pathfinding.path));
                break;
            }
        }
    }

    // Generate a list of positions adjacent to the player's position
    List<Vector3> GetAdjacentPositions(Vector3 playerPosition)
    {
        List<Vector3> adjacentPositions = new List<Vector3>
        {
            new Vector3(playerPosition.x + pathfinding.nodeSize, playerPosition.y, playerPosition.z),
            new Vector3(playerPosition.x - pathfinding.nodeSize, playerPosition.y, playerPosition.z),
            new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + pathfinding.nodeSize),
            new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - pathfinding.nodeSize)
        };

        return adjacentPositions;
    }

    IEnumerator MoveAlongPath(List<Node> path)
    {
        isMoving = true;
        foreach (Node node in path)
        {
            Vector3 targetPosition = new Vector3(node.gridX * pathfinding.nodeSize, transform.position.y, node.gridY * pathfinding.nodeSize);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        isMoving = false;
    }

}
