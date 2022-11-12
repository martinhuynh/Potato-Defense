using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private HotbarManager hotbarManager;
    [SerializeField]
    private ItemEnum type;
    [SerializeField]
    private Select selectAsset;

    [SerializeField]
    private TextMeshProUGUI quantity;


    [SerializeField]
    private GameObject item;

    

    



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

    


    public void use()
    {
        PlayerInventory.use(type);
        quantity.text = PlayerInventory.getInventory(type) + "";
        if (isAvailable()) return;
    }

    public bool isAvailable()
    {
        Color c = item.GetComponent<SpriteRenderer>().color;

        if (PlayerInventory.isAvailable(type))
        {
            c.a = 255;
            item.GetComponent<SpriteRenderer>().color = c;
            return true;
        }
        c.a = 0;
        item.GetComponent<SpriteRenderer>().color = c;
        return false;
    }





    // Start is called before the first frame update
    void Start()
    {
        isAvailable();
        quantity.text = PlayerInventory.getInventory(type) + "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
