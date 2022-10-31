using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public int[,] BackpackItems = new int[5,GetComponent<ShopManagerScript>().shopItemNumber]; 
    public int BackpackItemNumber = 0;
    public int[] Hand;

    // Start is called before the first frame update
    void Start()
    {
        Hand[1] = 0;
        Hand[3] = 0;

        //Item ID's =1
        //Quantity =3
        for(int i=0;i<=GetComponent<ShopManagerScript>().shopItemNumber;i++){
            BackpackItems[1, i] = 0;
            BackpackItems[3, i] = 0;        
        }


    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void load(){
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        for(int i=0;i<=GetComponent<ShopManagerScript>().shopItemNumber;i++){
            if(GetComponent<ShopManagerScript>().shopItems[3,i]>0){
                bool found = false;
                for(int j=0;j<=BackpackItemNumber;j++){
                    if(BackpackItems[1,j]==i){
                        BackpackItems[3,j]+=GetComponent<ShopManagerScript>().shopItems[3,i];
                        found = true;
                    } 
                }
                if(!found){
                    BackpackItemNumber++;
                    BackpackItems[1,BackpackItemNumber]=i;
                    BackpackItems[3,BackpackItemNumber]=GetComponent<ShopManagerScript>().shopItems[3,i];
                }
            }
        }
        
        for(int i=0;i<=shopItemNumber;i++){
            shopItems[3,i]=0;
        }
    }




}
