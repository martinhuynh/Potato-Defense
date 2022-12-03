using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class ShopManagerScript : MonoBehaviour
{

    public static int[,] shopItems = new int[5,5];
    //number stands for number of item to have in shop, one extra just in case
    //first number is for the items' ID in the first colum, second number is for items' price in second colum

    public PlayerStats playerStatsRef;
    public TextMeshProUGUI CoinsTXT;
    public TextMeshProUGUI SkillPointTXT;
    private HotbarManager hotbarManager;

    // Start is called before the first frame update
    void Start()
    {
        CoinsTXT.text = "Coins:" + PlayerInventory.potatoes.ToString();
        PlayerStats.restart();
        
        //Item ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;


        //Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 999;
        shopItems[2, 3] = 999;


        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;

        hotbarManager = GameObject.Find("Hotbar").GetComponent<HotbarManager>();
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (PlayerInventory.potatoes >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]) //check if player have enough coins for that item
        {
            PlayerInventory.potatoes -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]; //subtract coins
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++; //increse item quantity by 1
            CoinsTXT.text = "Coins:" + PlayerInventory.potatoes.ToString();    //update text when purchase
            
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
        refreshQuantity();
    }

    void refreshQuantity()
    {
        PlayerInventory.fence = shopItems[3, 1];
        hotbarManager.refreshItem();
    }

    void Update()
    {
        loadSkillpoint();
        // Inefficient. Should be called when shop is opened on on every update.
        SkillPointTXT.text = "Skill Points: " + PlayerStats.skillPoint.ToString();
        CoinsTXT.text = "Coins:" + PlayerInventory.potatoes.ToString();
    }

    private void OnEnable()
    {
        

    }
    public void loadSkillpoint()
    {
        //mskillPoints = playerStatsRef.returnSkillPoint();
        //if (mskillPoints > 0) Debug.Log("skill point loaded to the shop manager");
    }
}
