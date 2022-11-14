using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject up, down, left, right;
    private int hp;
    private Vector3Int gridPos;
    private ItemManager itemManager;

    public void start(Vector3Int gridPos, ItemManager itemManager)
    {
        this.gridPos = gridPos;
        this.itemManager = itemManager;
        hp = 10;
        Color temp = new Color(255, 255, 255, 0);
        up.GetComponent<SpriteRenderer>().color = temp;
        down.GetComponent<SpriteRenderer>().color = temp;
        right.GetComponent<SpriteRenderer>().color = temp;
        left.GetComponent<SpriteRenderer>().color = temp;

        UpdateOrder();
    }

    public void UpdateOrder()
    {
        int order = (int)(-100 * transform.position.y);
        GetComponent<SpriteRenderer>().sortingOrder = order;
        up.GetComponent<SpriteRenderer>().sortingOrder = order;
        down.GetComponent<SpriteRenderer>().sortingOrder = order;
        left.GetComponent<SpriteRenderer>().sortingOrder = order;
        right.GetComponent<SpriteRenderer>().sortingOrder = order;
    }

    public bool decrease(int power)
    {
        hp -= power;
        if (hp <= 0)
        {
            itemManager.remove(gridPos);
            itemManager.updateLink(gridPos, this);
            Destroy(this.gameObject);
            return false;
        }
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
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

    public void connect(Fence link)
    {
        Color temp = new Color(255, 255, 255, 255);
        temp.a = 255;
        switch (link)
        {
            case Fence.UP:
                up.GetComponent<SpriteRenderer>().color = temp;
                break;
            case Fence.DOWN:
                down.GetComponent<SpriteRenderer>().color = temp;
                break;
            case Fence.RIGHT:
                right.GetComponent<SpriteRenderer>().color = temp;
                break;
            case Fence.LEFT:
                left.GetComponent<SpriteRenderer>().color = temp;
                break;
        }
        UpdateOrder();

    }

    public void disconnect(Fence link)
    {
        Color temp = new Color(255, 255, 255, 255);
        temp.a = 0;
        switch (link)
        {
            case Fence.UP:
                up.GetComponent<SpriteRenderer>().color = temp;
                break;
            case Fence.DOWN:
                down.GetComponent<SpriteRenderer>().color = temp;
                break;
            case Fence.RIGHT:
                right.GetComponent<SpriteRenderer>().color = temp;
                break;
            case Fence.LEFT:
                left.GetComponent<SpriteRenderer>().color = temp;
                break;
        }
        UpdateOrder();

    }

}
