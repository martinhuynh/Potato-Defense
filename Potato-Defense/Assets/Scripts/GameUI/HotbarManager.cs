using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarManager : MonoBehaviour
{
    [SerializeField]
    private ItemSlot[] itemSlots;

    private Dictionary<KeyCode, ItemSlot> itemKeys;
    public static ItemSlot selected;
    public static bool use = false;
    [SerializeField]
    SelectBehavior selectBehavior;
    [SerializeField]
    GameObject repair;
    [SerializeField]
    GameObject place;
    private int current = 0;


    [SerializeField]
    private ItemEnum type;

    // Start is called before the first frame update
    void Start()
    {
        selected = itemSlots[0];
        itemKeys = new Dictionary<KeyCode, ItemSlot>();

        itemKeys.Add(KeyCode.Alpha1, itemSlots[0]);
        itemKeys.Add(KeyCode.Alpha2, itemSlots[1]);
        itemKeys.Add(KeyCode.Alpha3, itemSlots[2]);
        itemKeys.Add(KeyCode.Alpha4, itemSlots[3]);
        select(selected);

        itemSlots[0].key.text = "1";
        itemSlots[1].key.text = "2";
        itemSlots[2].key.text = "3";
        itemSlots[3].key.text = "4";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            current = (current + 1) % itemKeys.Count;
            select(itemSlots[current]);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            current = (current - 1 < 0) ? itemKeys.Count - 1 : current - 1;
            select(itemSlots[current]);
        }
        else if (Input.GetKeyDown(KeyCode.H))
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
        use = false;
        selected = exclude;
        selectBehavior.gameObject.SetActive(exclude.type == ItemEnum.DELETE);
        repair.gameObject.SetActive(exclude.type == ItemEnum.REPAIR);
        place.gameObject.SetActive(exclude.type == ItemEnum.FENCE);
    }

    public ItemEnum getSelected()
    {
        return selected.type;
    }

    

    // Call whenever a purchase is made.
    public void refreshItem()
    {

    }
}