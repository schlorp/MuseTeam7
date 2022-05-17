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
    [SerializeField] private int damage;
    [SerializeField] private float attackradius;
    [SerializeField] private float attacktime;

    [Header("Privates")]
    private GameObject target;
    private NavMeshAgent agent;
    private Pathfinding pathfinding;
    private bool move = true;
    private bool attacking = false;
    
    [Header("Publics")]
    [HideInInspector]public bool scan = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfinding = GetComponent<Pathfinding>();
    }

    void Update()
    {
        //scans for the player every frame
        if (scan)
        {
            ScanningArea();
        }
        if (pathfinding.seenplayer)
        {
            ChargeAtTarget(target);
        }
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
                }
            }
        }
    }

    private void ChargeAtTarget(GameObject target)
    {
        //get target and charge towards the target
        Vector3 chargepoint = target.transform.position;
        
        gameObject.transform.LookAt(chargepoint);
        if (move)
        {
            gameObject.transform.Translate(transform.forward * sprintspeed * Time.deltaTime);
        }

        //check if you hit the target
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackradius, detectionmask);

        for (int i = 0; i < colliders.Length; i++)
        {
            //if hit and player start the attack
            if (colliders[i].CompareTag("Player")&& !attacking)
            {
                move = false;
                StartCoroutine(Attack(colliders[i].gameObject));
            }
        }
    }

    private IEnumerator Attack(GameObject player)
    {
        //get the health and deal the damage after a few seconds(will become the seconds of the animation)
        attacking = true;
        PlayerHealth playerHP = player.GetComponent<PlayerHealth>();

        yield return new WaitForSeconds(attacktime);
        //play anim
        playerHP.TakeDamage(damage);

        //stop scanning and can move again
        move = true;
        target = null;
        scan = false;

        //start patrolling again
        pathfinding.seenplayer = false;
        pathfinding.Patrolling();
        pathfinding.Resume();
        attacking = false;
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackradius);
    }
}
