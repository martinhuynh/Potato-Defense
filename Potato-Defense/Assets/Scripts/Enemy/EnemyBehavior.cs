using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float health;
    private HealthbarBehavior hb;

    SoundEffectManager sound;

    void Start()
    {
        health = maxHealth;
        hb = this.GetComponentInChildren<HealthbarBehavior>();
        hb.UpdateHealthBar(health, maxHealth);

        sound = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<SoundEffectManager>();

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

        sound.playAttack();
        if (health <= 0)
        {
            sound.playDie();
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
