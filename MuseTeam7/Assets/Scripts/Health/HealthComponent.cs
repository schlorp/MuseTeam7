using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int starthealth;
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;


    [HideInInspector]public bool isenemy;
    
    void Start()
    {
        if (!isenemy)
        {
            health = starthealth;
            healthBar.SetMaxHealth(starthealth);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    virtual protected void Die()
    {

    }
}
