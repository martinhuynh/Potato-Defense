using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private HotbarManager hotbarManager;
    [SerializeField]
    private ItemEnum type;
    [SerializeField]
    private Select selectAsset;

    [SerializeField]
    private ItemQuantity color;

    



    private bool selected;

    public void select()
    {
        Debug.Log(type + " selected");
        selectAsset.select();
    }

    public void unselect()
    {
        
        selectAsset.unselect();
    }

    public void overZero()
    {
        Debug.Log(type + " selected");
        color.overZero();
    }

    public void Zero()
    {
        
        color.Zero();
    }


    public void use()
    {
        PlayerInventory.use(type);

        if (isAvailable()) return;
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = 0;
        GetComponent<SpriteRenderer>().color = c;
    }

    public bool isAvailable()
    {
        if (PlayerInventory.isAvailable(type))
        {
            color.overZero();
            return true;
        }
        color.Zero();
        return false;
    }





    // Start is called before the first frame update
    void Start()
    {
        isAvailable();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
