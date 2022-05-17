using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float detectionradius;
    [SerializeField] private LayerMask detectionmask;
    [Range(0f, -180f)]
    [SerializeField] public float minimumdetectionangle;
    [Range(0f, 180f)]
    [SerializeField] public float maximumdetectionangle;
    [SerializeField] private float sprintspeed;
    [SerializeField] private float Damage;
    [SerializeField] private float attackradius;

    [Header("Privates")]
    private GameObject target;
    private NavMeshAgent agent;
    private Pathfinding pathfinding;
    private bool move = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfinding = GetComponent<Pathfinding>();
    }

    void Update()
    {
        //scans for the player every frame
        ScanningArea();
    }

    private void ScanningArea()
    {
        //the list of obejcts inside the range and in the layermask
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionradius, detectionmask);
        //loop through them to get player
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.CompareTag("Player"))
            {
                //confirm if it is the player
                Vector3 targetdirection = colliders[i].transform.position - transform.position;
                //get the diffrence between the target and our forward pos(getting the angle)
                //if it is inside the line of sight the player is detected
                float viewableAngle = Vector3.Angle(targetdirection, transform.forward);
                if (viewableAngle > minimumdetectionangle && viewableAngle < maximumdetectionangle)
                {
                    //starts charging at the player
                    target = colliders[i].gameObject;
                    pathfinding.Stop();
                    pathfinding.seenplayer = true;
                    ChargeAtTarget(target);
                }
            }
        }
    }

    private void ChargeAtTarget(GameObject target)
    {
        Vector3 chargepoint = target.transform.position;
        
        gameObject.transform.LookAt(chargepoint);
        if (move)
        {
            gameObject.transform.Translate(transform.forward * sprintspeed * Time.deltaTime);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackradius, detectionmask);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                move = false;
                Attack();
            }
        }
    }

    private void Attack()
    {
        //get healtcomponent
        //dealdamage to the players health
        //do this when healthcomponent is there
        //play anim

        //start move again 
        //and scan again
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackradius);
    }
}
