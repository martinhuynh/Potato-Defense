using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI QuantityTxt;
    public GameObject ShopManager;

    public Button button;
    public Color wantedColor;
    public Color originalColor;
    private ColorBlock cb;

  

    void Start()
    {
        cb = button.colors;
        //originalColor = cb.selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
       // PriceTxt.text = "$" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = "Quantity: " + ShopManagerScript.shopItems[3, ItemID].ToString();
    }

    public void changeWhenHover()
    {
        cb.selectedColor = wantedColor;
        button.colors = cb;
    }

    public void changeWhenLeave()
    {
        cb.selectedColor = originalColor;
        button.colors = cb;
    }
}
