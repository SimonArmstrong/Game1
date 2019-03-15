using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour {
    public Vector2[,] grid;
    public int width, height;
    public int pixelsPerUnit = 16;


    public void Awake()
    {
        grid = new Vector2[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Vector2(x * pixelsPerUnit, y * pixelsPerUnit);
            }
        }
    }
    /*
    public Vector3 GetNearestNode(Vector3 pos) {
        for (int i = 0; i < grid.Length; i++) {
            
        }
    }*/
}
