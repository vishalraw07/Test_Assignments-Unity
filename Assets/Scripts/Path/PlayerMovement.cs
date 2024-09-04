using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Pathfinding pathfinding;
    private bool isMoving;

    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Vector3 targetPosition = GetMouseWorldPosition();
            pathfinding.FindPath(transform.position, targetPosition);
            if (pathfinding.path != null)
            {
                StartCoroutine(MoveAlongPath(pathfinding.path));
            }
        }
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

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

}
