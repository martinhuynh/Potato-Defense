using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static int fence = 10, lure = 0;

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
        return false;
    }

}
