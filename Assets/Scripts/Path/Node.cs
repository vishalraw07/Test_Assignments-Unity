using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;
    public bool walkable;
    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost { get { return gCost + hCost; } }

    public Node(bool walkable, int x, int y)
    {
        this.walkable = walkable;
        gridX = x;
        gridY = y;
    }
}
