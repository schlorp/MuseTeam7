using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogThrower : MonoBehaviour
{
    public int range;
    public LayerMask layer;
    public int force;
    public GameObject kickpoint;
    public GameObject deatheffect;


    private Rigidbody _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        kickpoint = GameObject.Find("Kickpoint");
    }

    private void Update()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, range, layer);

        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Vector3 dir = transform.position - kickpoint.transform.position;
                Debug.DrawRay(transform.position, dir);

                _rb.AddForce(dir * force);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            FindObjectOfType<audioManager>().Play("Dog_Death");
            FindObjectOfType<audioManager>().RandomSounds();
            Dogcounter.instance.deadDogs += 1;
            Score.scoreValue += 1;
            Instantiate(deatheffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
