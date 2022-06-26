using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogDeathEffect : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject,5);
    }
}
