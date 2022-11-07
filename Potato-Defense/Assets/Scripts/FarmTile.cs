using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Handle growing crops if a seed is planted.
 */
public class FarmTile : MonoBehaviour
{
    private Vector3Int position;
    private TileData tileData;
    private FarmManager farmManager;
    public int current = 0;
    private int hp;

    private float completeTime = 2.0f;
    // Start is called before the first frame update

    void Start()
    {
    }

    public void use(Vector3Int position, TileData tileData, FarmManager fm)
    {
        this.position = position;
        this.tileData = tileData;
        this.farmManager = fm;
    }

    public void update()
    {

    }

    public void decreaseHP(int amount)
    {
        hp -= amount;
        if (hp < 0) hp = 0;
    }

    //// Only harvest when crop is done.
    //public bool harvest()
    //{
    //    // Check if it is at the last stage aka done.
    //    if ( current != farmManager.getStages() - 1) return false;
    //    //PlayerInventory.potatos++;
    //    Destroy(this);
    //    return true;
    //}

    //public void OnDestroy()
    //{
    //    StopCoroutine(grow_crop());
    //    Destroy(this);
    //}

    
}
