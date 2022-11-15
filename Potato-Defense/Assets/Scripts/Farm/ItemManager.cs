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

    // if gridPos exists then it will link with neighboring fences.
    // if not then disconnect neighboring fences.
    public void updateLink(Vector3Int gridPos, FenceBehavior fence)
    {
        // true = link, false = unlink.
        bool linkOrUnlink = fences.ContainsKey(gridPos);
        Vector3Int temp = gridPos;
        temp.x += 1; // RIGHT
        if (fences.ContainsKey(temp))
        {
            //Debug.Log(gridPos + " " + temp);
            if (linkOrUnlink)
            {
                fence.connect(Fence.RIGHT);
                fences[temp].connect(Fence.LEFT);
            } else
            {
                fences[temp].disconnect(Fence.LEFT);
            }
        }

        temp.x -= 2; // LEFT
        if (fences.ContainsKey(temp))
        {
            if (linkOrUnlink)
            {
                fence.connect(Fence.LEFT);
                fences[temp].connect(Fence.RIGHT);
            }
            else
            {
                fences[temp].disconnect(Fence.RIGHT);
            }
        }

        temp.x += 1;
        temp.y -= 1; // DOWN
        if (fences.ContainsKey(temp))
        {
            if (linkOrUnlink)
            {
                fence.connect(Fence.DOWN);
                fences[temp].connect(Fence.UP);
            }
            else
            {
                fences[temp].disconnect(Fence.UP);
            }
        }

        temp.y += 2; // UP
        if (fences.ContainsKey(temp))
        {
            if (linkOrUnlink)
            {
                fence.connect(Fence.UP);
                fences[temp].connect(Fence.DOWN);
            }
            else
            {
                fences[temp].disconnect(Fence.DOWN);
            }
        }
    }

    public bool place(Vector3 pos)
    {
        Vector3Int gridPos = map.WorldToCell(pos);
        if (fences.ContainsKey(gridPos)) return false;
        if (!mapManager.isAvailable(pos)) return false;
        FenceBehavior fence = Instantiate(fencePrefab);
        fence.start(gridPos, this);
        fence.transform.position = map.GetCellCenterWorld(gridPos);
        fences.Add(gridPos, fence);

        updateLink(gridPos, fence);

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
    
    // Getters
    public FenceBehavior getFence(Vector3Int fencePos)
    {
        return fences[fencePos];
    }

    public bool containsFence(FenceBehavior fence)
    {
        return fences.ContainsValue(fence);
    }
}
