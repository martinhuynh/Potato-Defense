using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfind
{
    TileMapManager map;
    Tilemap groundMap;
    Dictionary<Vector3, PathfindTileData> dict;

    public Pathfind()
    {
        map = GameObject.Find("GameManager").GetComponent<TileMapManager>();
        groundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
        dict = new Dictionary<Vector3, PathfindTileData>();

        BoundsInt bounds = groundMap.cellBounds;

        for (int y = bounds.min.y - 1; y < bounds.max.y + 1; y++)
        {
            for (int x = bounds.min.x - 1; x < bounds.max.x + 1; x++)
            {
                Vector3Int tileLoc = new Vector3Int(x, y);
                Vector3 cellPos = groundMap.GetCellCenterWorld(tileLoc);
                dict[cellPos] = new PathfindTileData(cellPos);
            }
        }
    }

    public List<PathfindTileData> FindPath(PathfindTileData start, PathfindTileData end)
    {
        List<PathfindTileData> openList = new List<PathfindTileData>();
        List<PathfindTileData> closedList = new List<PathfindTileData>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            PathfindTileData currentTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if (currentTile.gridLocation == end.gridLocation)
            {
                return getFinishedList(start, currentTile);
            }

            List<PathfindTileData> neighbourTiles = GetNeighbourTiles(currentTile);

            foreach(PathfindTileData neighbour in neighbourTiles)
            {
                if (listContains(closedList, neighbour)) continue;

                neighbour.G = getManhattenDistance(start, neighbour);
                neighbour.H = getManhattenDistance(end, neighbour);

                neighbour.previous = currentTile;

                if (!listContains(openList, neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return new List<PathfindTileData>();
    }

    private List<PathfindTileData> getFinishedList(PathfindTileData start, PathfindTileData end)
    {
        List<PathfindTileData> finishedList = new List<PathfindTileData>();

        PathfindTileData currentTile = end;

        while (currentTile.gridLocation != start.gridLocation)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile.previous;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private bool listContains(List<PathfindTileData> list, PathfindTileData tile)
    {
        foreach(PathfindTileData n in list)
        {
            if (n.gridLocation == tile.gridLocation) return true;
        }
        return false;
    }

    private float getManhattenDistance(PathfindTileData start, PathfindTileData neighbour)
    {
        return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Mathf.Abs(start.gridLocation.y - neighbour.gridLocation.y);
    }

    private List<PathfindTileData> GetNeighbourTiles(PathfindTileData currentTile)
    {
        List<PathfindTileData> neighbours = new List<PathfindTileData>();

        if (map.isWalkable(currentTile.gridLocation, Action.UP))
        {
            if (dict.ContainsKey(currentTile.gridLocation + Vector3Int.up))
                neighbours.Add(dict[currentTile.gridLocation + Vector3Int.up]);
        }
        if (map.isWalkable(currentTile.gridLocation, Action.RIGHT))
        {
            if (dict.ContainsKey(currentTile.gridLocation + Vector3Int.right))
                neighbours.Add(dict[currentTile.gridLocation + Vector3Int.right]);
        }
        if (map.isWalkable(currentTile.gridLocation, Action.DOWN))
        {
            if (dict.ContainsKey(currentTile.gridLocation + Vector3Int.down))
                neighbours.Add(dict[currentTile.gridLocation + Vector3Int.down]);
        }
        if (map.isWalkable(currentTile.gridLocation, Action.LEFT))
        {
            if (dict.ContainsKey(currentTile.gridLocation + Vector3Int.left))
                neighbours.Add(dict[currentTile.gridLocation + Vector3Int.left]);
        }
        return neighbours;
    }
}
