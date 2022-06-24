using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   
    public static void GoToGameOverScene()
    {
        SceneManager.LoadScene("Game Over");
    }
    public static void Back()
    {
        SceneManager.LoadScene("Menu");
    }

}
