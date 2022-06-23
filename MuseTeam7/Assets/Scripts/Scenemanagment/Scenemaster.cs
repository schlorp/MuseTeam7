using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemaster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string[] _rooms;


    [Header("Private")]
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetData();
    }

    public void Home()
	{
        FindObjectOfType<audioManager>().Play("ResortBack");
        SceneManager.LoadScene("ResortScene");
        EditData();
    }
    public void Options()
	{
        //SceneManager.LoadScene("Options");
        Debug.Log("Loads options");
	}

    public void Room1()
    {
        SceneManager.LoadScene("Room1");
        EditData();
    }

    public void Room2()
    {
        SceneManager.LoadScene("Room2");
        EditData();
    }
    public void Room3()
    {
        SceneManager.LoadScene("Room3");
        EditData();
    }
    public void Exit() // exits the game
    {
        Debug.Log("Quiting!");
        Application.Quit();
    }

    public void EditData()
    {
        InventoryObject.instance.HP = player.GetComponent<PlayerHealth>().GetHealth();
        InventoryObject.instance.keys = player.GetComponentInChildren<Inventory>().Getkeys();
    }

    public void SetData()
    {
        player.GetComponent<PlayerHealth>().SetHealth(InventoryObject.instance.HP);
        player.GetComponentInChildren<Inventory>().Setkeys(InventoryObject.instance.keys);
    }
}
