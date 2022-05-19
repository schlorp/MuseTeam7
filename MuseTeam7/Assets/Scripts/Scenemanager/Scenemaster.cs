using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemaster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string[] rooms;

    public string GetRoom(int roomID)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i == roomID)
            {
                SceneManager.LoadScene(rooms[i]);
            }
        }
        return rooms[0];
    }

    public void Room1()
    {
        SceneManager.LoadScene("Room1");
    }
}
