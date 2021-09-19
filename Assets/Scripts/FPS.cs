using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text fpsText;
    float deltaTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        ShowFrames();
    }

    void ShowFrames()
    {
        float msec = deltaTime * 100.0f;
        float frames = 1.0f / deltaTime;
        string txt = string.Format("{0:0.0} ms  ({1:0.}) fps", msec, frames);
        fpsText.text = txt;
    }
}
