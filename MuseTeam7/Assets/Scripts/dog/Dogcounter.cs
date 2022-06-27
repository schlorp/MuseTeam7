using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dogcounter : MonoBehaviour
{
    public static Dogcounter instance;
    public int deadDogs;
    public TextMeshProUGUI killScore;

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

	private void Update()
	{
        killScore.text = deadDogs.ToString();
	}

}
