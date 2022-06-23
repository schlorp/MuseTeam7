using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static bool[] keys;

	private void Start()
	{
		Debug.Log(keys.Length);
		keys[0] = true;
	}

	public bool[] Getkeys()
    {
		return keys;
    }
	public void Setkeys(bool[] _keys)
    {
		keys = _keys;
    }

}
