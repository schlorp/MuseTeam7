using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]private Vector3 waypoint;
    [SerializeField]private float waypointradius;
    [SerializeField] private bool validwaypoint;

    void Start()
    {
        validwaypoint = false;
        agent = GetComponent<NavMeshAgent>();
        FindWaypoint();
    }

    void Update()
    {
        Patrolling();
    }

    private void Patrolling()
    {
        Vector3 distance = transform.position - waypoint;
        if (validwaypoint)
        {
            agent.SetDestination(waypoint);// setting the destination once
        }
        if (distance.magnitude < 1.5f) //check if distance to walkpoint is under certain number and set a new one if conditions met
        {
            validwaypoint=false;
            FindWaypoint();
        }
    }

    private void FindWaypoint()
    {
        // grabbing a random position in a circle around the AI

        float randomX = Random.Range(-waypointradius, waypointradius);
        float randomZ = Random.Range(-waypointradius, waypointradius);

        waypoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        Vector3 direction = waypoint - transform.position;
        Physics.Raycast(transform.position, direction, out RaycastHit hit);
        float distance = hit.distance;

        if (distance < waypointradius)
        {
            if (hit.transform.tag == "unwalkable")
            {
                validwaypoint = false;
                FindWaypoint();
            }
        }
        else if (distance > waypointradius)
        {
            validwaypoint = true;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(waypoint, 2);
    }
}
