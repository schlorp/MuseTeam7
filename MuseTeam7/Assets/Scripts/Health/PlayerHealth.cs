using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerHealth : HealthComponent
{
    protected override void Die()
    {
        base.Die();
            
        Destroy(gameObject, 3);

        GameOver();
    }

    public void GameOver()
    {
        FindObjectOfType<audioManager>().Stop();
        FindObjectOfType<audioManager>().Play("PlayerDeath");
        SceneManager.LoadScene("GameOver");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Debug.Log("loading scene");
    }

	private void OnTriggerEnter(Collider other)
	{
        TakeDamage(2000000000);
        SceneManager.LoadScene("Room3GameOver");
        FindObjectOfType<audioManager>().Play("PlayerDeath");
        Cursor.lockState= CursorLockMode.Confined;
        Cursor.visible = true;
        Debug.Log("hit");
	}
}
