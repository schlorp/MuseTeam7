using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int _starthealth;
    [SerializeField] private int _health;
    [SerializeField] private HealthBar _healthBar;

    [Header("Public")]
    [HideInInspector] public bool isenemy;


    
    void Start()
    {
        _health = _starthealth;
        if (!isenemy)
        {
            _healthBar.SetMaxHealth(_starthealth);
        }
    }


    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (!isenemy)
        {
            _healthBar.SetHealth(_health);
        }

        if (_health <= 0)
        {
            Die();
        }
    }

    virtual protected void Die()
    {

    }
    public int GetHealth()
    {
        return _health;
    }
    public void SetHealth(int _health)
    {
        this._health = _health;
    }
}
