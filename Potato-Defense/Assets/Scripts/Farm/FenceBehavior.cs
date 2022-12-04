using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject up, down, left, right;
    private int hp, startHP;
    private Vector3Int gridPos;
    private ItemManager itemManager;
    private Color opacity;

    public void start(Vector3Int gridPos, ItemManager itemManager)
    {
        this.gridPos = gridPos;
        this.itemManager = itemManager;
        hp = 20;
        startHP = hp;
        opacity = GetComponent<SpriteRenderer>().color;
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
        GetComponent<SpriteRenderer>().sortingOrder = order - 1;
        up.GetComponent<SpriteRenderer>().sortingOrder = order;
        down.GetComponent<SpriteRenderer>().sortingOrder = order;
        left.GetComponent<SpriteRenderer>().sortingOrder = order;
        right.GetComponent<SpriteRenderer>().sortingOrder = order;
    }

    private void setOpacity(float alpha)
    {
        opacity.a = alpha;
        GetComponent<SpriteRenderer>().color = opacity;
        if (up.GetComponent<SpriteRenderer>().color.a != 0) up.GetComponent<SpriteRenderer>().color = opacity;
        if (down.GetComponent<SpriteRenderer>().color.a != 0) down.GetComponent<SpriteRenderer>().color = opacity;
        if (right.GetComponent<SpriteRenderer>().color.a != 0) right.GetComponent<SpriteRenderer>().color = opacity;
        if (left.GetComponent<SpriteRenderer>().color.a != 0) left.GetComponent<SpriteRenderer>().color = opacity;
    }

    public bool decrease(int power)
    {
        hp -= power;
        float opacity = (float)hp / (float)startHP;
        setOpacity(opacity);
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            return false;
        }
        return true;
    }

    public void OnDestroy()
    {
        itemManager.remove(gridPos);
        itemManager.updateLink(gridPos, this);
        
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
            //Debug.Log("Enter");
            decrease(5);
        }
    }

    public void repair()
    {
        hp += (int) PlayerStats.repair;
        if (hp > startHP) hp = startHP;
        setOpacity((float)hp / (float)startHP);
    }

    public void connect(Fence link)
    {
        switch (link)
        {
            case Fence.UP:
                up.GetComponent<SpriteRenderer>().color = opacity;
                break;
            case Fence.DOWN:
                down.GetComponent<SpriteRenderer>().color = opacity;
                break;
            case Fence.RIGHT:
                right.GetComponent<SpriteRenderer>().color = opacity;
                break;
            case Fence.LEFT:
                left.GetComponent<SpriteRenderer>().color = opacity;
                break;
        }
        UpdateOrder();

    }

    public void disconnect(Fence link)
    {
        Color temp = new Color(255, 255, 255, 0);
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
