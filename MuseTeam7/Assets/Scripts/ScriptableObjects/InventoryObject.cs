using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryObject : MonoBehaviour
{
    public int HP;
    public bool[] keys;
    public static InventoryObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        HP = 100;
        keys = new bool[3];
        DontDestroyOnLoad(gameObject);

      
    }  
   
}
