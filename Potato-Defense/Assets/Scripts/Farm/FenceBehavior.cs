using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehavior : MonoBehaviour
{
    private int hp;
    private Vector3Int gridPos;
    private ItemManager itemManager;

    public void start(Vector3Int gridPos, ItemManager itemManager)
    {
        this.gridPos = gridPos;
        this.itemManager = itemManager;
        hp = 10;
    }

    public void decrease(int power)
    {
        hp -= power;
        if (hp <= 0)
        {
            itemManager.remove(gridPos);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().sortingOrder = (int)(-100 * transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Enter");
            decrease(5);
        }
    }

}
