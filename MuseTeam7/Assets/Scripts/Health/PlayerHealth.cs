using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    protected override void Die()
    {
        base.Die();
        FindObjectOfType<audioManager>().Play("PlayerDeath");
        Destroy(gameObject);
    }
}
