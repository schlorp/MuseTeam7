using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float spawnradius;
    [SerializeField] private GameObject[] Enemies;

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
        float randomX = Random.Range(-spawnradius, spawnradius);
        float randomZ = Random.Range(-spawnradius, spawnradius);

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
        float randomenemy = Random.Range(0,Enemies.Length);
        for (int i = 0; i < Enemies.Length; i++)
        {
            if(i == randomenemy)
            {
                currentenemy = Enemies[i];
            }
        }

        Instantiate(currentenemy, spawnpoint, transform.rotation);
    }

}
