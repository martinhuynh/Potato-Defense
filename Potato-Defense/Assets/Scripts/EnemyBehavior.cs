using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float maxHealth = 10f;
    private float health;
    private HealthbarBehavior hb;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hb = this.GetComponentInChildren<HealthbarBehavior>();
        hb.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Testing purposes
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(2f);
    }

    private void FixedUpdate()
    {
        TakeDamage(0.5f * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hb.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
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
