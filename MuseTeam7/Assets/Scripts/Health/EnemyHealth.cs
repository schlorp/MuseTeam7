using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class EnemyHealth : HealthComponent
{
    public Animator animator;

    private Pathfinding _path;
    private CombatAI _com;
    private NavMeshAgent _agent;
    protected override void Die()
    {
        base.Die();
        _path.enabled = false;
        _com.enabled = false;
        _agent.baseOffset = 0;
        animator.SetBool("Die", true);
        Destroy(gameObject, 2.6f);
    }

    private void Awake()
    {
        _path = GetComponent<Pathfinding>();
        _com = GetComponent<CombatAI>();
        _agent = GetComponent<NavMeshAgent>();
        isenemy = true;
    }

}

