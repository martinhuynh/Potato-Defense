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
    private KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };


    [SerializeField]
    private ItemEnum type;

    // Start is called before the first frame update
    void Start()
    {
        selected = itemSlots[0];
        itemKeys = new Dictionary<KeyCode, ItemSlot>();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemKeys.Add(keys[i], itemSlots[i]);
            itemSlots[i].key.text = i + 1 + "";
        }
        select(selected);
        refreshItem();
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

        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                selected = itemKeys[key];
                select(selected);
                break;
            }
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
        Debug.Log("Refresh. " + PlayerInventory.fence);
        itemSlots[1].transform.Find("Item").GetComponent<SpriteRenderer>().color = (PlayerInventory.fence > 0) ? new Color(255, 255, 255, 1f) : new Color(255, 255, 255, 0.4f);
    }
}
