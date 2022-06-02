using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
            if (isPaused)
			{
                Resume(); // resumes the game if paused
			}
            else
			{
                Pause(); // pauses the game
			}
		}
    }

    public void Resume() // resume function
	{
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // resumes game
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
	{
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freezes game
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMenu() // loads menu
	{
        Debug.Log("Menu loading");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
	}

    public void Exit() // exits the game
	{
        Debug.Log("Quiting!");
        Application.Quit();
	}
}
