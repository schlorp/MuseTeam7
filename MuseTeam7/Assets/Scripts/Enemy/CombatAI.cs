using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float detectionradius;
    [SerializeField] private LayerMask detectionmask;

    
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
            }
        }
    }
}
