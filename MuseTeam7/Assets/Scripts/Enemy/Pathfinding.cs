using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{

    [Header("NavMesh")]
    private NavMeshAgent agent;

    [Header("Waypoints")]
    [SerializeField] private float waypointradius;
    private Vector3 waypoint;
    private bool validwaypoint;

    [Header("bools")]
    [SerializeField] private bool raycastshot;
    public bool seenplayer;

    [Header("Other")]
    private CombatAI combatAI;

    void Awake()
    {
        seenplayer = false;
        validwaypoint = false;
        combatAI = GetComponent<CombatAI>();
        agent = GetComponent<NavMeshAgent>();
        FindWaypoint();
    }

    void Update()
    {
        //patrols if he has not seen the player
        if (!seenplayer)
        {
            Patrolling();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            FindWaypoint();
        }
    }

    public void Patrolling()
    {
        Vector3 distance = transform.position - waypoint;
        if (validwaypoint)
        {
            agent.SetDestination(waypoint);// setting the destination once the waypoint is valid
        }
        if (distance.magnitude < 2f) //check if distance to walkpoint is under certain number and set a new one if conditions met
        {
            Debug.Log("new waypoint");
            combatAI.scan = true;
            validwaypoint = false;
            FindWaypoint();
        }
    }

    public void SetWaypoint(Vector3 _waypoint)
    {
        waypoint = _waypoint;
    }

    public void FindWaypoint()
    {
        // grabbing a random position in a circle around the AI

        float randomX = Random.Range(-waypointradius, waypointradius);
        float randomZ = Random.Range(-waypointradius, waypointradius);

        waypoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        Vector3 direction = waypoint - transform.position;
        raycastshot = Physics.Raycast(transform.position, direction, out RaycastHit hit);
        Debug.DrawRay(transform.position, direction, Color.red);
        //check if the raycast hit something
        if (raycastshot)
        {
            float distance = hit.distance;

            //check if the waypoint is in the wall
            //if it is it is gonna scan for a new one

            if (distance < waypointradius)
            {
                if (hit.transform.CompareTag("unwalkable"))
                {
                    validwaypoint = false;
                    FindWaypoint();
                }
            }
            else
            {
                validwaypoint = true;
            }
        }
        else
        {
            validwaypoint = true;
        }
    }

    public void Stop()
    {
        agent.isStopped = true;
    }
    public void Resume()
    {
        agent.isStopped=false;
    }
}
