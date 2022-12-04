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
    private Tile grass;

    [SerializeField]
    private TileMapManager mapManager;

    [SerializeField]
    private HotbarManager hotbarManager;

    private Dictionary<Vector3Int, CropBehavior> crops;

    private WaveSystem waveSystem;

    private readonly int credit = 10;

    private bool grow = false;

    // Start is called before the first frame update
    void Start()
    {
        crops = new Dictionary<Vector3Int, CropBehavior>();
        waveSystem = GameObject.Find("WaveSystem").GetComponent<WaveSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {
            grow = !grow;
            toggleGrowth(grow);
        }
    }

    public void delete(Vector3 pos)
    {
        Vector3Int gridPos = map.WorldToCell(pos);
        if (crops.Count > 1 && crops.ContainsKey(gridPos))
        {
            //Debug.Log(gridPos);
            map.SetTile(gridPos, grass);
            Destroy(crops[gridPos].gameObject);
            crops.Remove(gridPos);
        }
    }

    public bool isAvailable(Vector3Int pos)
    {
        return !crops.ContainsKey(pos);
    }

    public bool isOnlyPlowed(Vector3Int pos)
    {
        return !crops.ContainsKey(pos) || crops[pos].getState() != Farm.GROWING && crops[pos].getState() != Farm.DONE;
    }

    // true = grow, false = stop
    public void toggleGrowth(bool grow)
    {
        foreach (KeyValuePair <Vector3Int, CropBehavior> crop in crops)
        {
            crop.Value.toggleGrowth(grow);
        }
        Debug.Log(grow);
        this.grow = grow;
    }

    public bool isGrowing()
    {
        return grow;
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
        newCrop.setPosition(map.GetCellCenterWorld(gridPosition));
        plant(position);
    }

    public void harvest(Vector3 position)
    {
        Vector3Int gridPosition = map.WorldToCell(position);
        crops[gridPosition].harvest();
        //Debug.Log("Harvested: " + PlayerInventory.potatoes);
        PlayerInventory.potatoes += credit;
        hotbarManager.refreshItem();
        map.SetTile(gridPosition, dirt);
        waveSystem.increasePotatoes();
        plant(position);
    }

    // Plant at position.
    public bool plant(Vector3 position)
    {
        Vector3Int gridPos = map.WorldToCell(position);
        map.SetTile(gridPos, plowed);
        crops[gridPos].startGrowing(this, gridPos, mapManager.GetTileData(gridPos));
        return true;
    }

    public bool destroyCrop(Vector3Int position)
    {
        if (crops.Count == 1) return false;
        Destroy(crops[position].gameObject);
        crops.Remove(position);
        map.SetTile(position, grass);
        return true;
    }

    public bool destroyCrop(Vector3 position)
    {
        Vector3Int gridPos = map.WorldToCell(position);
        return destroyCrop(gridPos);
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
