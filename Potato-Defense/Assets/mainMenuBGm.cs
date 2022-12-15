using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuBGm : MonoBehaviour
{
    public bool bgmStatus = true;
    public AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playBGM()
    {
        bgm.Play();
    }
    public void stopBGM()
    {
        bgm.Stop();
    }
    public void changeBoolStatus()
    {
        if (bgmStatus == true)
        {
            bgmStatus = false;
            stopBGM();
        }
        else
        {
            bgmStatus = true;
            playBGM();
        }
    }
}
