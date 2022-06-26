using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dogcounter : MonoBehaviour
{
    public static Dogcounter instance;
    public int deadDogs;

	private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        deadDogs = 0;
    }
}
