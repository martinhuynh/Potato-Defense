using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindTileData
{
    public float G;
    public float H;
    public float F { get { return G + H; } }

    public bool isBlocked;

    public PathfindTileData previous;

    public Vector3 gridLocation;

    public PathfindTileData(Vector3 gridLoc)
    {
        gridLocation = gridLoc;
    }
}
