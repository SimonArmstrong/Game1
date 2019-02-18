using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour
{
    PathRequestManager requestManager;

    GridScript grid;
    
    void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        //grid = GetComponent<GridScript>();
    }

    private void Start() {
        
    }

    void Update()
    {
        //FindPath(seeker.position, target.position);
        //HandlePathfindingLOD();
    }


    public void StartFindPath(Vector2 startPos, Vector2 targetPos)
    {
        grid = GameManager.instance.curGrid;
        StartCoroutine(FindPath(startPos, targetPos));
    }

    public void EndFindPath()
    {
        StopAllCoroutines();
        requestManager.FinishProcessingPath(new Vector2[0], false);
    }

    #region PathfindingLOD
    /*
     *bool stallBool = false;
    void HandlePathfindingLOD()
    {
        float dst = Vector2.Distance(seeker.position, target.position);
        if(dst > 7)
        {
            if (!stallBool)
            {
                stallBool = true;
                StopAllCoroutines();
                //StartCoroutine(FindPathAfterDelay(0.5f));
            }
        }
        if(dst > 3 && dst <= 7)
        {
            if (!stallBool)
            {
                stallBool = true;
                StopAllCoroutines();
                //StartCoroutine(FindPathAfterDelay(0.3f));
            }
        }
        if(dst <= 3)
        {
            if (!stallBool)
            {
                stallBool = true;
                StopAllCoroutines();
                //StartCoroutine(FindPathAfterDelay(0.1f));
            }
        }
    }
    */
#endregion

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector2[] waypoints = new Vector2[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Debug.Log("Start Node worldPos: " + startNode.worldPos + ",   Target Node worldPos:" + targetNode.worldPos);

        if (startNode.walkable && targetNode.walkable)
        {

            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node curNode = openSet.RemoveFirst();
                closedSet.Add(curNode);

                if (curNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(curNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = curNode.GCost + GetDistance(curNode, neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = curNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        if (waypoints.Length == 0) pathSuccess = false;
        requestManager.FinishProcessingPath(waypoints, pathSuccess);
    }

    Vector2[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        if (path.Count == 0) return new Vector2[0];
        Vector2[] waypoints = SimplifyPath(path);//
        Array.Reverse(waypoints);

        return waypoints;

    }

    Vector2[] SimplifyPath(List<Node> path)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;
        waypoints.Add(path[0].worldPos);//
        for (int i = 1; i < path.Count - 1; i++)
        {
            Vector2 directionNew = new Vector2(path[i].gridX - path[i + 1].gridX, path[i].gridY - path[i + 1].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }

        if (path.Count > 1)
            waypoints.Add(path[path.Count - 1].worldPos);
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}