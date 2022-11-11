using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQuantity : MonoBehaviour
{

    [SerializeField]
    private PlayerInventory Inventory;

    private Text quantity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void overZero()
    {
        quantity=Inventory.getInventory();

        Color c = GetComponent<SpriteRenderer>().color;
        c.a = 255;
        GetComponent<SpriteRenderer>().color = c;
    }

    public void Zero()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = 0;
        GetComponent<SpriteRenderer>().color = c;
    }
}
