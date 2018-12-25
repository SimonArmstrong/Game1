using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {

    public bool walkable;
    public Vector2 worldPos;
    public int gridX;
    public int gridY;
    public int movementPenalty;

    public int HCost;
    public int GCost;
    public Node parent;
    int heapIndex;

    public Node(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY, int _penalty)
    {
        walkable = _walkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _penalty;
    }

    public int FCost
    {
        get
        {
            return GCost + HCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if(compare == 0)
        {
            compare = HCost.CompareTo(nodeToCompare.HCost);
        }
        return -compare;
    }
}
