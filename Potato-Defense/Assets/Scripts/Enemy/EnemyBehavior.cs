using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float health;
    private HealthbarBehavior hb;

    public AudioSource hittingSoundEffect;
    public AudioSource dieSoundEffect;

    /*    public AudioClip hittingSoundEffect;
        public AudioClip dieSoundEffect;
        public AudioSource audio;*/

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hb = this.GetComponentInChildren<HealthbarBehavior>();
        hb.UpdateHealthBar(health, maxHealth);

        /*hittingSoundEffect = gameObject.AddComponent<AudioSource>();
        dieSoundEffect  = gameObject.AddComponent<AudioSource>();*/

/*        AudioSource[] audioSources = GetComponents<AudioSource>();
        audio = audioSources[0];
        hittingSoundEffect = audioSources[0].clip;
        dieSoundEffect = audioSources[1].clip;*/
    }

    // Update is called once per frame
    void Update()
    {
        // Testing purposes
        //if (Input.GetKeyDown(KeyCode.K)) TakeDamage(2f);
    }

    private void FixedUpdate()
    {
        GetComponent<Renderer>().sortingOrder = (int) (-100 * transform.position.y);
    }

    public bool TakeDamage(float damage)
    {
        health -= damage;
        hb.UpdateHealthBar(health, maxHealth);

        // Debug.Log("damage taken");
        // hittingSoundEffect.Play();
        if (health > 0)
        {
            hittingSoundEffect.Play();
            //audio.PlayOneShot(hittingSoundEffect, 0.1F);
        }
        else
        {
            dieSoundEffect.Play();
            //audio.PlayOneShot(dieSoundEffect, 0.1F);
        }
        
        if (health <= 0)
        {
            //dieSoundEffect.Play();
            
            Die();
            
            return true;
        }
        return false;


    }

    public void Die()
    {
       
        if (gameObject.activeSelf)
        {           
            gameObject.SetActive(false);
            
            Destroy(gameObject);
        }     
    }

    // Getters
    public float getMaxHealth()
    {
        return maxHealth;
    }
    
    public float getHealth()
    {
        return health;
    }
}
