using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    // Grass.
    [SerializeField]
    public Tilemap groundMap;

    // Load in tiles with their properties
    [SerializeField]
    private List<TileData> tileDatas;

    // Search up tile properties.
    private Dictionary<TileBase, TileData> dataFromTiles;

    [SerializeField]
    private ItemManager itemManager;

    [SerializeField]
    private FarmManager farmManager;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    public bool isAvailable(Vector3 pos, Action direction)
    {
        Vector3Int gridPos = getNewPosition(pos, direction);
        return itemManager.isAvailable(gridPos) == true && farmManager.isAvailable(gridPos);
    }

    public bool isAvailable(Vector3 pos)
    {
        Vector3Int gridPos = groundMap.WorldToCell(pos);
        return itemManager.isAvailable(gridPos) == true && farmManager.isAvailable(gridPos);
    }

    public bool isWalkable(Vector3 pos, Action direction)
    {
        return itemManager.isAvailable(getNewPosition(pos, direction));
    }

    private Vector3Int getNewPosition(Vector3 pos, Action direction)
    {
        Vector3Int gridPos = groundMap.WorldToCell(pos);
        switch (direction)
        {
            case Action.UP:
                gridPos.y++;
                break;
            case Action.DOWN:
                gridPos.y--;
                break;
            case Action.LEFT:
                gridPos.x--;
                break;
            case Action.RIGHT:
                gridPos.x++;
                break;
        }
        return gridPos;
    }

    public TileData GetTileData(Vector3Int tilePosition)
    {
        TileBase tile = groundMap.GetTile(tilePosition);
        return (tile == null) ? null : dataFromTiles[tile];
    }


    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void setDirt(Vector3 position)
    {
        //tilemap.SetTile(new Vector3Int((int)Mathf.Floor(position.x), (int)Mathf.Floor(position.y), (int)Mathf.Floor(position.z)), dirt);
    }
}
