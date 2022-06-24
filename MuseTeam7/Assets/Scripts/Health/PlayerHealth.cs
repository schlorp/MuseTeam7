using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    protected override void Die()
    {
        base.Die();
            
        Destroy(gameObject, 1);
    }
}
