using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    public AudioSource music;
    public GameObject gameUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        music.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameUI.SetActive(true);
    }

    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        music.Pause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameUI.SetActive(false);
    }

    public void Menu()
    {
        //Time.timeScale = 1f;
        //load scene
    }

    public void Quit()
    {
        Application.Quit();
    }
}
