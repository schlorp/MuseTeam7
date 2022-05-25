using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _attackthreshold;
    [SerializeField] private Transform _meleepoint;
    [SerializeField] private float _animationspeed;
    [SerializeField] private int _damage;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Slash());
        }
    }

    public IEnumerator Slash()
    {
        yield return new WaitForSeconds(_animationspeed);

        Collider[] colliders = Physics.OverlapSphere(_meleepoint.transform.position,_attackthreshold);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Debug.Log("Slash");
                collider.GetComponent<EnemyHealth>().TakeDamage(_damage);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_meleepoint.position, _attackthreshold);
    }
}
