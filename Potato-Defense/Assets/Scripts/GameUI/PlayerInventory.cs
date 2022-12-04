using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static HotbarManager hotbarManager;
    public static int fence = 0, lure = 0, item3 = 0, item4 = 0, potatoes = 10;

    private void Start()
    {
        
    }

    public static void use(ItemEnum type)
    {
        if (type == ItemEnum.FENCE)
        {
            PlayerInventory.fence--;
        }
        else if (type == ItemEnum.LURE)
        {
            PlayerInventory.lure--;
        }
        else if (type == ItemEnum.ITEM3)
        {
            PlayerInventory.item3--;
        }
        else if (type == ItemEnum.ITEM4)
        {
            PlayerInventory.item4--;
        }
        hotbarManager = GameObject.Find("Hotbar").GetComponent<HotbarManager>();
        hotbarManager.refreshItem();
    }

    public static bool isAvailable(ItemEnum type)
    {
        if (type == ItemEnum.FENCE)
        {
            return fence != 0;
        }
        else if (type == ItemEnum.LURE)
        {
            return lure != 0;
        }
        else if (type == ItemEnum.ITEM3)
        {
            return item3 != 0;
        }
        else if (type == ItemEnum.ITEM4)
        {
            return item4 != 0;
        }
        else if (type == ItemEnum.REPAIR)
        {
            return true;
        }
        else if (type == ItemEnum.DELETE)
        {
            return true;
        }
        else if (type == ItemEnum.HOE)
        {
            return true;
        }
        return false;
    }


    public static int getInventory(ItemEnum type)
    {
        if (type == ItemEnum.FENCE)
        {
            return PlayerInventory.fence;
        }
        else if (type == ItemEnum.LURE)
        {
            return PlayerInventory.lure;
        }
        else if (type == ItemEnum.ITEM3)
        {
            return PlayerInventory.item3;
        }
        else if (type == ItemEnum.ITEM4)
        {
            return PlayerInventory.item4;
        }else return -1;
    }

}
