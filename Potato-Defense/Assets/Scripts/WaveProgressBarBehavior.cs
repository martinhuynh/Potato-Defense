using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgressBarBehavior : MonoBehaviour
{
    public Slider slider;
    public Color green;
    public Color yellow;
    public Color red;

    private float progressSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        slider.value += progressSpeed * Time.deltaTime;
        if (slider.value >= 50f && slider.value < 75f) slider.fillRect.GetComponent<Image>().color = yellow;
        else if (slider.value >= 75f) slider.fillRect.GetComponent<Image>().color = red;
        else slider.fillRect.GetComponent<Image>().color = green;
    }
}
