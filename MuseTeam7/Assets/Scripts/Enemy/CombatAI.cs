using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class CombatAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float detectionradius;
    [SerializeField] private LayerMask _detectionmask;
    [Range(0f, -180f)]
    [SerializeField] public float minimumdetectionangle;
    [Range(0f, 180f)]
    [SerializeField] public float maximumdetectionangle;
    [SerializeField] private float sprintspeed;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackradius;
    [SerializeField] private float _attacktime;


    [Header("Privates")]
    private GameObject _target;
    private Pathfinding _pathfinding;
    private bool _move = true;
    private bool _attacking = false;
    private NavMeshAgent _agent;
    private PlayerController _controller;


    [Header("Publics")]
    [HideInInspector]public bool scan = true;
    public Animator animator;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _pathfinding = GetComponent<Pathfinding>();
        _controller = FindObjectOfType<PlayerController>();
        Debug.Log(_controller);
    }

    void Update()
    {
        //scans for the player every frame
        if (scan)
        {
            ScanningArea();
        }
        if (_pathfinding.seenplayer)
        {
            ChargeAtTarget(_target);
        }
    }

    private void ScanningArea()
    {
        //the list of obejcts inside the range and in the layermask
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionradius, _detectionmask);
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
                    //check if there is a obejct in the view
                    Ray ray = new Ray(transform.position, targetdirection);
                    if(Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Debug.DrawRay(transform.position,targetdirection, Color.red);
                        if (!hit.transform.CompareTag("unwalkable"))
                        {
                            //starts charging at the player
                            _target = colliders[i].gameObject;
                            _pathfinding.Stop();
                            _pathfinding.seenplayer = true;
                        }
                    }
                }
            }
        }
    }

    private void ChargeAtTarget(GameObject target)
    {
        animator.SetBool("Sprint", true);
        //get target and charge towards the target
        Vector3 chargepoint = target.transform.position;

        gameObject.transform.LookAt(chargepoint);

        /*
        Vector3 dir = chargepoint - gameObject.transform.position;
        Quaternion torotation = Quaternion.FromToRotation(transform.forward, dir);
        transform.rotation = torotation;
        */
        if (_move)
        {
            transform.position = Vector3.MoveTowards(transform.position, chargepoint, sprintspeed * Time.deltaTime);
        }

        //check if you hit the target
        Collider[] colliders = Physics.OverlapSphere(transform.position, _attackradius, _detectionmask);

        for (int i = 0; i < colliders.Length; i++)
        {
            //if hit and player start the attack
            if (colliders[i].CompareTag("Player")&& !_attacking)
            {
               
                _move = false;
                StartCoroutine(Attack(colliders[i].gameObject));
            }
        }
    }

    private IEnumerator Attack(GameObject player)
    {
        animator.SetBool("Attack", true);
        _controller.enabled = false;
        //get the health and deal the damage after a few seconds(will become the seconds of the animation)
        _attacking = true;
        PlayerHealth playerHP = player.GetComponent<PlayerHealth>();

        yield return new WaitForSeconds(_attacktime - 1);
        //play anim
        playerHP.TakeDamage(_damage);
        yield return new WaitForSeconds(1);

        //stop scanning and can move again
        _move = true;
        _target = null;
        scan = false;

        //start patrolling again
        _pathfinding.seenplayer = false;
        _pathfinding.Patrolling();
        _pathfinding.Resume();
        _attacking = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Sprint", false);
        _controller.enabled = true;
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackradius);
    }
}
