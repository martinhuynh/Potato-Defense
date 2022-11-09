using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    private Vector3Int position;
    private TileData tileData;
    private FarmManager farmManager;
    private int hp;
    [SerializeField]
    private Sprite stage_1, stage_2, stage_done;

    private Queue<Sprite> stages;
    // Start is called before the first frame update
    public void startGrowing(FarmManager fm, Vector3Int position, TileData tileData)
    {
        stages = new Queue<Sprite>();
        stages.Enqueue(null);
        stages.Enqueue(stage_2);
        stages.Enqueue(stage_done);
        farmManager = fm;
        this.tileData = tileData;
        hp = tileData.hp;
        this.position = position;
        StartCoroutine(grow_crop());
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().sortingOrder = (int)(-100 * transform.position.y);
    }

    public bool decrease(int power)
    {
        hp -= power;
        if (hp <= 0) { 
            farmManager.destroyCrop(position); 
            Destroy(this.gameObject); 
            StopCoroutine(grow_crop());
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
        for (int i = 0; i < size; i++)
        {
            // Update sprite
            Sprite newSprite = stages.Dequeue();
            GetComponent<SpriteRenderer>().sprite = newSprite;
            yield return new WaitForSeconds(1f);
        }
    }

    // Getters
    public Vector3Int getPosition()
    {
        return position;
    }
}
