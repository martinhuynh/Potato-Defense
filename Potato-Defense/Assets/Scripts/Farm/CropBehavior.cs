using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropBehavior : MonoBehaviour
{
    private Vector3Int position;
    private TileData tileData;
    private FarmManager farmManager;
    private int hp, startHP;
    [SerializeField]
    private Sprite stage_1, stage_2, stage_done;
    private Farm state;
    private Color opacity;

    private Queue<Sprite> stages;
    [SerializeField]
    private Tile plowed;

    private WaveSystem waveSystem;
    private Tilemap map;

    public void startGrowing(FarmManager fm, Vector3Int position, TileData tileData)
    {
        stages = new Queue<Sprite>();
        stages.Enqueue(null);
        stages.Enqueue(stage_2);
        stages.Enqueue(stage_done);
        farmManager = fm;
        this.tileData = tileData;
        hp = tileData.hp;
        startHP = hp;
        opacity = GetComponent<SpriteRenderer>().color;
        this.position = position;
        GetComponent<Renderer>().sortingOrder = (int)(-100 * transform.position.y);
        StartCoroutine(grow_crop());
    }

    private void Start()
    {
        waveSystem = GameObject.Find("WaveSystem").GetComponent<WaveSystem>();
        map = GameObject.Find("Ground").GetComponent<Tilemap>();
        //plowed = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Tilesets/TilePalette/Dirt/Tilled Dirt_0.asset", typeof(Tile)) as Tile;
        harvest();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            decrease(2);
        }
    }

    public void harvest()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        state = Farm.PLOWED;
        PlayerInventory.potatoes++;
    }

    public Farm getState()
    {
        return state;
    }

    private void setOpacity(float alpha)
    {
        opacity.a = alpha;
        GetComponent<SpriteRenderer>().color = opacity;
    }

    public bool decrease(int power)
    {
        hp -= power;
        setOpacity((float)hp / (float)startHP);
        if (hp <= 0) {
            GetComponent<SpriteRenderer>().sprite = null;
            state = Farm.PLOWED;
            map.SetTile(position, plowed);
            StopCoroutine(grow_crop());
            waveSystem.decreaseLives();
            return false;
        }
        return true;
    }

    public bool done()
    {
        return stages.Count == 0;
    }

    public void OnDestroy()
    {
        StopCoroutine(grow_crop());
    }

    public IEnumerator grow_crop()
    {
        int size = stages.Count;
        state = Farm.GROWING;
        for (int i = 0; i < size; i++)
        {
            // Update sprite
            Sprite newSprite = stages.Dequeue();
            GetComponent<SpriteRenderer>().sprite = newSprite;
            GetComponent<SpriteRenderer>().color = opacity;
            if (size - 1 == i) break;
            yield return new WaitForSeconds(10f);
        }
        state = Farm.DONE;
    }

    // Getters
    public Vector3Int getPosition()
    {
        return position;
    }
}
