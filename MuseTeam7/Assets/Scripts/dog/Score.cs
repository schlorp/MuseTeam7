using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
	public static int scoreValue;
    public TextMeshProUGUI score;

	private void Start()
	{
		//score = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		scoreValue = Dogcounter.instance.deadDogs;
		//score.text = scoreValue.ToString();
		score.text = "Dead dogs : " + scoreValue;
	}
}
