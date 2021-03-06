﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

    public bool displayGridGizmos;

    public LayerMask unwalkableLayer;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public TerrainType[] walkableRegions;
    LayerMask walkableMask;
    Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

    public Node[,] grid;

    GameManager gameManager;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    public void Init()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

        foreach(TerrainType region in walkableRegions)
        {
            walkableMask.value |= region.terrainMask.value;
            if(!walkableRegionsDictionary.ContainsKey((int)Mathf.Log(region.terrainMask.value, 2.0f)))
                walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.value, 2.0f),region.terrainPenalty);
        }

        CreateGrid();

        GameManager.instance.UpdateCurGrid(this);
    }

    public int MaxSize {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    public void ClearGrid()
    {
        displayGridGizmos = false;
        grid = new Node[0, 0];
    }

    void CreateGrid()
    {
        displayGridGizmos = true;
        grid = new Node[gridSizeX, gridSizeY];
        //Debug.Log(grid.Length);
        Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableLayer));
                int movementPenalty = 0;

                if (walkable)
                {
                    RaycastHit2D hit = Physics2D.Raycast(worldPoint + Vector2.up * 50, Vector2.down, 100, walkableMask);
                    if (hit.collider != null)
                    {
                        walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                    }
                }

                grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector2 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
        if (grid != null && displayGridGizmos)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? new Color(1, 1, 1, .3f) : new Color(1, 0, 0, 0.3f);
                Gizmos.DrawWireSphere(n.worldPos, nodeRadius);
            }
        }
    }
[System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }
}
