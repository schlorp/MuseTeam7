using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthComponent
{
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
