using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource buyAndUpgrade;
    public AudioSource attack;
    public AudioSource die;
    public AudioSource jump;
    public AudioSource placeFense;
    public AudioSource plow;
    public AudioSource harvest;
    public AudioSource hitPlants;
    public AudioSource attackAir;
    public AudioSource bgm;

    public bool bgmStatus = true;
    //private float bgmVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void playBAU()
    {
        buyAndUpgrade.Play();
    }
    public void playAttack()
    {
        attack.Play();
    }
    public void playDie()
    {
        die.Play();
    }
    public void playJump()
    {
        jump.Play();
    }
    public void playPlaceFense()
    {
        placeFense.Play();
    }
    public void playPlow()
    {
        plow.Play();
    }
    public void playHarvest()
    {
        harvest.Play();
    }
    public void playHitPlants()
    {
        hitPlants.Play();
    }
    public void playAttackAir()
    {
        attackAir.Play();
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
        if(bgmStatus == true)
        {
            bgmStatus = false;
            stopBGM();
        }
        else
        {
            bgmStatus =true;
            playBGM();
        }
    }

}
