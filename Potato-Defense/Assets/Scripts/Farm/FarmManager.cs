using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


// Stores all the tiles related to farming (plow, plant, harvest)
public class FarmManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private CropBehavior crop;

    [SerializeField]
    private Tile dirt;

    [SerializeField]
    private Tile plowed;

    [SerializeField]
    private TileMapManager mapManager;

    private Dictionary<Vector3Int, CropBehavior> crops;

    private WaveSystem waveSystem;

    // Start is called before the first frame update
    void Start()
    {
        crops = new Dictionary<Vector3Int, CropBehavior>();
        waveSystem = GameObject.Find("WaveSystem").GetComponent<WaveSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isAvailable(Vector3Int pos)
    {
        return !crops.ContainsKey(pos);
    }

    public bool isOnlyPlowed(Vector3Int pos)
    {
        return !crops.ContainsKey(pos) || crops[pos].getState() != Farm.GROWING && crops[pos].getState() != Farm.DONE;
    }

    public Farm getState(Vector3 pos)
    {
        Vector3Int gridPos = map.WorldToCell(pos);
        return (crops.ContainsKey(gridPos)) ? crops[gridPos].getState() : Farm.GRASS;
    }

    // Check if tilebase is grass.
    public void plow(Vector3 position)
    {
        Vector3Int gridPosition = map.WorldToCell(position);
        map.SetTile(gridPosition, dirt);
        CropBehavior newCrop = Instantiate(crop);
        crops.Add(gridPosition, newCrop);
        newCrop.transform.position = map.GetCellCenterWorld(gridPosition);
    }

    //// Check if tile plantable.
    //public bool plowable(Vector3 position)
    //{
    //    Vector3Int gridPosition = map.WorldToCell(position);
    //    return mapManager.GetTileData(gridPosition).state == TileType.GRASS;
    //}

    //public bool harvestable(Vector3 position)
    //{
    //    Vector3Int gridPosition = map.WorldToCell(position);
    //    CropBehavior crop = crops[gridPosition];
    //    return crop.done();
    //}

    public void harvest(Vector3 position)
    {
        Vector3Int gridPosition = map.WorldToCell(position);
        crops[gridPosition].harvest();
        Debug.Log("Harvested: " + PlayerInventory.potatoes);
        map.SetTile(gridPosition, dirt);
        waveSystem.decreaseTarget();
    }

    // Plant at position.
    public bool plant(Vector3 position)
    {
        Vector3Int gridPos = map.WorldToCell(position);
        map.SetTile(gridPos, plowed);
        crops[gridPos].startGrowing(this, gridPos, mapManager.GetTileData(gridPos));
        return true;
    }

    public void destroyCrop(Vector3Int position)
    {
        Destroy(crops[position].gameObject);
        crops.Remove(position);
    }

    public void harvest(Vector3Int position)
    {
        //PlayerInventory.potatoes++;
        destroyCrop(position);
    }

    // Getters
    public Dictionary<Vector3Int, CropBehavior> getCrops()
    {
        return crops;
    }

    public CropBehavior getCropAt(Vector3Int pos)
    {
        if (!crops.ContainsKey(pos)) return null;
        return crops[pos];
    }
}
