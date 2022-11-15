using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseSystem : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        text.text = "YOU WIN";
    }

    public void Lose()
    {
        text.text = "GAME OVER";
    }
}
