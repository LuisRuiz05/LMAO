using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitToMainMenu());
    }

    // Update is called once per frame
    IEnumerator WaitToMainMenu()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene(1);
    }
}
