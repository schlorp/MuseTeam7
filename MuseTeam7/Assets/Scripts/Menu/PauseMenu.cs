using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject ReadyScreen;
    public GameObject OptionsUI;
	private void Start()
	{
        ReadyScreen.SetActive(true);
        Freeze();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

	// Update is called once per frame
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
            if (isPaused)
			{
                Resume(); // resumes the game if paused
                Cursor.lockState = CursorLockMode.None;

                Cursor.visible = false;
			}
            else
			{
                Pause(); // pauses the game
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
		}
    }

    public void Resume() // resume function
	{
        FindObjectOfType<audioManager>().Play("Theme");
        FindObjectOfType<audioManager>().ResumeMusic();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // resumes game
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Freeze() // freezes game on start
	{
        FindObjectOfType<audioManager>().PauseMusic();
        Time.timeScale = 0f;
        isPaused = true;
       
	}
     
    void Pause()
	{   
        FindObjectOfType<audioManager>().PauseMusic();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freezes game
        isPaused = true;
        Cursor.visible = true;
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
    public void Ready()
	{
        Debug.Log("Start Game");
        ReadyScreen.SetActive(false);
        Cursor.visible = false;
        Resume();
    }

    public void Restart()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("resetting");
	}

    public void Opties()
	{
        OptionsUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CloseOptions()
	{
        OptionsUI.SetActive(false);
    }

    
}
