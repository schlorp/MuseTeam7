using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

    public HealthBar healthBar; 

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        curHealth -= amount;
        Debug.Log("Hit!");

        healthBar.SetHealth(curHealth);
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.I))
		{
            TakeDamage(20);
		}
	}

}
