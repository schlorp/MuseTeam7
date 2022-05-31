using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class EnemyHealth : HealthComponent
{
    public Animator animator;

    private Pathfinding path;
    private CombatAI com;
    private NavMeshAgent agent;
    protected override void Die()
    {
        base.Die();
        path.enabled = false;
        com.enabled = false;
        agent.baseOffset = 0;
        animator.SetBool("Die", true);
        Destroy(gameObject, 2.6f);
    }

    private void Awake()
    {
        path = GetComponent<Pathfinding>();
        com = GetComponent<CombatAI>();
        agent = GetComponent<NavMeshAgent>();
        isenemy = true;
    }
}
