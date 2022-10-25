using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManagerScript : MonoBehaviour
{

    public int[,] shopItems = new int[5,5]; 
    //number stands for number of item to have in shop, one extra just in case
    //first number is for the items' ID in the first colum, second number is for items' price in second colum
    
    public float coins;
    public Text CoinsTXT;
    
    // Start is called before the first frame update
    void Start()
    {
        CoinsTXT.text = "Coins:" + coins.ToString();
        
        //Item ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price
        shopItems[2, 1] = 100;
        shopItems[2, 2] = 200;
        shopItems[2, 3] = 300;
        shopItems[2, 4] = 400;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]) //check if player have enough coins for that item
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]; //subtract coins
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++; //increse item quantity by 1
            CoinsTXT.text = "Coins:" + coins.ToString();    //update text when purchase
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
    }

    void Update()
    {
        
    }
}
