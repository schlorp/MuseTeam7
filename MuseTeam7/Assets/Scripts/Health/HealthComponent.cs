using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int starthealth;
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;

    [Header("Public")]
    [HideInInspector] public bool isenemy;


    
    void Start()
    {
        health = starthealth;
        if (!isenemy)
        {
            healthBar.SetMaxHealth(starthealth);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (!isenemy)
        {
            healthBar.SetHealth(health);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    virtual protected void Die()
    {

    }
}
