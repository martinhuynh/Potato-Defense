using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    [SerializeField]
    private ItemSlot[] itemSlots;

    private Dictionary<KeyCode, ItemSlot> itemKeys;
    private ItemSlot selected;


    [SerializeField]
    private ItemEnum type;

    
    



    
    // Start is called before the first frame update
    void Start()
    {
        selected = itemSlots[0];
        
        itemKeys = new Dictionary<KeyCode, ItemSlot>();

        itemKeys.Add(KeyCode.Alpha7, itemSlots[0]);
        itemKeys.Add(KeyCode.Alpha8, itemSlots[1]);
        itemKeys.Add(KeyCode.Alpha9, itemSlots[2]);
        itemKeys.Add(KeyCode.Alpha0, itemSlots[3]);
        select(selected);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (selected.isAvailable()) selected.use(); ;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selected = itemKeys[KeyCode.Alpha7];
            select(selected);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selected = itemKeys[KeyCode.Alpha8];
            select(selected);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            selected = itemKeys[KeyCode.Alpha9];
            select(selected);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selected = itemKeys[KeyCode.Alpha0];
            select(selected);
        }


    }

    private void select(ItemSlot exclude)
    {
        exclude.select();
        foreach (ItemSlot s in itemSlots)
        {
            if (s.Equals(exclude)) continue;
            s.unselect();
        }
    }


    

    // Call whenever a purchase is made.
    public void refreshItem()
    {

    }
}
