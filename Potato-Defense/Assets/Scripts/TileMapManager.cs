using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField]
    public static TileMapManager tileMapManager;
    public Tilemap tilemap;

    public TileBase dirt;

    public TileMapManager()
    {
        if (tileMapManager == null) tileMapManager = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void setDirt(Vector3 position)
    {
        Debug.Log(position);
        tilemap.SetTile(new Vector3Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y), (int)Mathf.Round(position.z)), dirt);
    }
}
