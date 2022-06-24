using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{

    [Header("NavMesh")]
    private NavMeshAgent _agent;

    [Header("Waypoints")]
    [SerializeField] private float _waypointradius;
    private Vector3 _waypoint;
    private bool _validwaypoint;

    [Header("bools")]
    [SerializeField] private bool _raycastshot;
    public bool seenplayer;

    [Header("Other")]
    private CombatAI _combatAI;

    void Start()
    {
        seenplayer = false;
        _validwaypoint = false;
        _combatAI = GetComponent<CombatAI>();
        _agent = GetComponent<NavMeshAgent>();
        FindWaypoint();
        FindObjectOfType<audioManager>().Play("EnemyWalk");


    }

    void Update()
    {
        //patrols if he has not seen the player
        if (!seenplayer)
        {
            Patrolling();
        }
    }

    public void Patrolling()
    {
        Vector3 distance = transform.position - _waypoint;
        if (_validwaypoint)
        {
            _agent.SetDestination(_waypoint);// setting the destination once the waypoint is valid
        }
        if (distance.magnitude < 2f) //check if distance to walkpoint is under certain number and set a new one if conditions met
        {
            Debug.Log("new waypoint");
            _combatAI.scan = true;
            _validwaypoint = false;
            FindWaypoint();
        }
    }

    public void SetWaypoint(Vector3 _waypoint)
    {
        this._waypoint = _waypoint;
    }

    public void FindWaypoint()
    {
        // grabbing a random position in a circle around the AI

        float randomX = Random.Range(-_waypointradius, _waypointradius);
        float randomZ = Random.Range(-_waypointradius, _waypointradius);

        _waypoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        Vector3 direction = _waypoint - transform.position;
        _raycastshot = Physics.Raycast(transform.position, direction, out RaycastHit hit);
        Debug.DrawRay(transform.position, direction, Color.red);
        //check if the raycast hit something
        if (_raycastshot)
        {
            float distance = hit.distance;

            //check if the waypoint is in the wall
            //if it is it is gonna scan for a new one

            if (distance < _waypointradius)
            {
                if (hit.transform.CompareTag("unwalkable"))
                {
                    _validwaypoint = false;
                    FindWaypoint();
                }
            }
            else
            {
                _validwaypoint = true;
            }
        }
        else
        {
            _validwaypoint = true;
        }
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }
    public void Resume()
    {
        _agent.isStopped=false;
    }
}
