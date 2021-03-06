using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public GameObject buttons;
    public bool isMainMenu = false;
    public Canvas controls;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        controls.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        loadingScreen.SetActive(true);
        buttons.SetActive(false);
        StartCoroutine(LoadAsynchronously());
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowControls()
    {
        controls.gameObject.SetActive(true);
    }

    public void CloseControls()
    {
        controls.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation;

        if (isMainMenu)
        {
            operation = SceneManager.LoadSceneAsync(2);
        }
        else
        {
            operation = SceneManager.LoadSceneAsync(3);
        }

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%"; 

            yield return null;
        }
    }
}
