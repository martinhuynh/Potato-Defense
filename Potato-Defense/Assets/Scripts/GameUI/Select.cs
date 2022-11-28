using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = 255;
        GetComponent<SpriteRenderer>().color = c;
    }

    public void unselect()
    {
        Color c = GetComponent<SpriteRenderer>().color;
        c.a = 0;
        GetComponent<SpriteRenderer>().color = c;
    }
}
