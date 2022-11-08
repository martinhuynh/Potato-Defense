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
