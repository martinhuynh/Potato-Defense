using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class itemScript : MonoBehaviour
{
    private GameObject item;
    private int health = 100;
    private int level =1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0) Destroy(gameObject);
    }

    private void upgrade(){
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if(level<10){
            health+=100;
            level++;
        }
        
    }
}
