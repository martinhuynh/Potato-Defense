using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
}
