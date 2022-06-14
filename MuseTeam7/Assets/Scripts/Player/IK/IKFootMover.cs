using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootMover : MonoBehaviour
{
    [Header("Privates")]
    private GameObject Body;
    private Vector3 currentpos;
    private Vector3 newpos;
    private Vector3 oldpos;
    private float lerp = 1;


    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float stepdistance;
    [SerializeField] private float stepheight;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Transform Raypoint;

    void Start()
    {
        newpos = transform.position;
        currentpos = transform.position;
    }


    private void Update()
    {
        Step();
    }

    private void Step()
    {
        transform.position = currentpos + new Vector3(0, .5f, 0);

        Ray ray = new Ray(Raypoint.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 10, LayerMask))
        {
            if (Vector3.Distance(newpos, hit.point) > stepdistance)
            {
                lerp = 0;
                newpos = hit.point;
                currentpos = newpos;
            }
        }
        if (lerp < 1)
        {
            Vector3 footposition = Vector3.Lerp(oldpos, newpos, lerp);
            footposition.y += Mathf.Sin(lerp * Mathf.PI) * stepheight;

            currentpos = footposition;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldpos = newpos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}

