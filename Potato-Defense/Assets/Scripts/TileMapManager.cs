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
        Debug.Log(new Vector3Int((int)Mathf.Floor(position.x), (int)Mathf.Floor(position.y), (int)Mathf.Floor(position.z)));
        tilemap.SetTile(new Vector3Int((int)Mathf.Floor(position.x), (int)Mathf.Floor(position.y), (int)Mathf.Floor(position.z)), dirt);
    }
}
