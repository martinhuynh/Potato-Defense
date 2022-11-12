using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private FenceBehavior fencePrefab;

    [SerializeField]
    private TileMapManager mapManager;

    // Temporary. only for fence right now.
    private Dictionary<Vector3Int, FenceBehavior> fences;

    public void remove(Vector3Int pos)
    {
        if (fences.ContainsKey(pos)) fences.Remove(pos);
    }

    public bool isAvailable(Vector3Int pos)
    {
        return !fences.ContainsKey(pos);
    }

    public bool place(Vector3 pos)
    {
        Vector3Int gridPos = map.WorldToCell(pos);
        TileData tileData = mapManager.GetTileData(gridPos);
        if (fences.ContainsKey(gridPos)) return false;
        if (tileData.state == TileType.PLOWED) return false;
        FenceBehavior fence = Instantiate(fencePrefab);
        fence.start(gridPos, this);
        fence.transform.position = map.GetCellCenterWorld(gridPos);
        fences.Add(gridPos, fence);

        Vector3Int temp = gridPos;
        temp.x += 1; // RIGHT
        if (fences.ContainsKey(temp))
        {
            Debug.Log(gridPos + " " + temp);
            fence.connect(Fence.RIGHT);
            fences[temp].connect(Fence.LEFT);
        }

        temp.x -= 2; // LEFT
        if (fences.ContainsKey(temp))
        {
            fence.connect(Fence.LEFT);
            fences[temp].connect(Fence.RIGHT);
        }

        temp.x += 1;
        temp.y -= 1; // DOWN
        if (fences.ContainsKey(temp))
        {
            fence.connect(Fence.DOWN);
            fences[temp].connect(Fence.UP);
        }

        temp.y += 2; // UP
        if (fences.ContainsKey(temp))
        {
            fence.connect(Fence.UP);
            fences[temp].connect(Fence.DOWN);
        }

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        fences = new Dictionary<Vector3Int, FenceBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeFence(Vector3Int pos)
    {

    }
}
