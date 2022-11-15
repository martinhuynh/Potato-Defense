using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveTimerText : MonoBehaviour
{
    WaveSystem waveSystem;
    TMP_Text textMP;

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
        if (!waveSystem.isInWave())
        {
            textMP.text = "Wave Starting in\n" + Mathf.Ceil(waveSystem.getGracePeriodEnd() - Time.time);
        }
        else
        {
            textMP.text = "";
        }
    }
}
