using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static int fence = 10, lure = 0, item3 = 0, item4 = 0;

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
        }else return 0;
    }

}
