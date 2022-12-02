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
    [SerializeField]
    private Tile grass;

    private WaveSystem waveSystem;
    private Tilemap map;

    private Coroutine thread = null;

    public void startGrowing(FarmManager fm, Vector3Int position, TileData tileData)
    {
        farmManager = fm;
        this.tileData = tileData;
        hp = tileData.hp;
        startHP = hp;
        this.position = position;
        startGrowing();
    }

    private void startGrowing()
    {
        GetComponent<Renderer>().sortingOrder = (int)(-100 * transform.position.y);
        setOpacity(1f);
        hp = startHP;
        state = Farm.GROWING;
        stages = new Queue<Sprite>();
        stages.Enqueue(stage_2);
        stages.Enqueue(stage_done);
        thread = StartCoroutine(grow_crop());
    }

    public void toggleGrowth(bool pause)
    {
        Debug.Log(pause);
        if (pause)
        {
            StopCoroutine(thread);
        } else
        {
            thread = StartCoroutine(grow_crop());
        }
    }
    

    private void Start()
    {
        waveSystem = GameObject.Find("WaveSystem").GetComponent<WaveSystem>();
        map = GameObject.Find("Ground").GetComponent<Tilemap>();
        opacity = new Color(255, 255, 255, 1f);
        //plowed = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Tilesets/TilePalette/Dirt/Tilled Dirt_0.asset", typeof(Tile)) as Tile;
        GetComponent<SpriteRenderer>().sprite = null;
        
        state = Farm.GROWING;
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
        //PlayerInventory.potatoes++;
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
            StopCoroutine(thread);
            waveSystem.decreaseLives();
            if (!farmManager.destroyCrop(transform.position))
            {
                startGrowing();
            }
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
        StopCoroutine(thread);
    }

    public IEnumerator grow_crop()
    {
        int size = stages.Count;
        state = Farm.GROWING;
        for (int i = 0; i < size; i++)
        {
            // Update sprite
            yield return new WaitForSeconds(10f);
            Sprite newSprite = stages.Dequeue();
            GetComponent<SpriteRenderer>().sprite = newSprite;
            GetComponent<SpriteRenderer>().color = opacity;
        }
        state = Farm.DONE;
    }

    // Getters
    public Vector3Int getPosition()
    {
        return position;
    }
}
