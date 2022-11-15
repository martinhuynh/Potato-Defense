using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PriceTxt;
    public Text QuantityTxt;
    public GameObject ShopManager;


    // Update is called once per frame
    void Update()
    {
        PriceTxt.text = "$" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
    }
}
