using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _spawnradius;
    [SerializeField] private GameObject[] _Enemies;

    [Header("Private")]
    private Vector3 spawnpoint;
    private GameObject currentenemy;
    void Start()
    {
        
    }

    void Update()
    {
       // if (Input.GetMouseButtonDown(0))
       // {
       //     FindSpawnpoint();
       // }
    }

    public void FindSpawnpoint()
    {
        //get a random spawnpoint
        float randomX = Random.Range(-_spawnradius, _spawnradius);
        float randomZ = Random.Range(-_spawnradius, _spawnradius);

        spawnpoint = new Vector3 (transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //checks if the spawnpos is in a object if it is it gets a new spawnpoint
        Collider[] colliders = Physics.OverlapSphere(spawnpoint, 1);

        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("unwalkable"))
            {
                FindSpawnpoint();
            }
            else
            {
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy()
    {
        float randomenemy = Random.Range(0,_Enemies.Length);
        for (int i = 0; i < _Enemies.Length; i++)
        {
            if(i == randomenemy)
            {
                currentenemy = _Enemies[i];
            }
        }

        Instantiate(currentenemy, spawnpoint, transform.rotation);
    }

}
