using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetText : MonoBehaviour
{
    WaveSystem waveSystem;
    TMP_Text textMP;
    private int targetAmount;
    // Start is called before the first frame update
    void Start()
    {
        waveSystem = GameObject.Find("WaveSystem").GetComponent<WaveSystem>();
        textMP = gameObject.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(waveSystem.getWavesLeft() == 2)
        {
            targetAmount = 10;
        }
        else if (waveSystem.getWavesLeft() == 1)
        {
            targetAmount = 15;
        }
        //int currentAmount = targetAmount - waveSystem.getTarget();
        textMP.text = waveSystem.getTarget() + " / "+ targetAmount.ToString();
       // textMP.text = "Target Potatoes Left: " + waveSystem.getTarget();
        //Debug.Log(currentAmount);
        
    }
}
