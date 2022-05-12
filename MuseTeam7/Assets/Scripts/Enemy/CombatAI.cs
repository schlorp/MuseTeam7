using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float detectionradius;
    [SerializeField] private LayerMask detectionmask;
    [Range(0f, -180f)]
    [SerializeField] public float minimumdetectionangle;
    [Range(0f, 180f)]
    [SerializeField] public float maximumdetectionangle;

    [Header("Gameobjects")]
    [SerializeField] private GameObject target;

    
    void Update()
    {
        ScanningArea();
    }

    private void ScanningArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionradius, detectionmask);

        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.CompareTag("Player"))
            {
                Vector3 targetdirection = colliders[i].transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetdirection, transform.forward);
                if (viewableAngle > minimumdetectionangle && viewableAngle < maximumdetectionangle)
                {
                    target = colliders[i].gameObject;
                    Debug.Log(target.name);
                }
            }
        }
    }
}
