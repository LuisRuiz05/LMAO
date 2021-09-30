using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preinteraction : MonoBehaviour
{
    public Canvas preinteractionCanvas;

    private void Start()
    {
        preinteractionCanvas.enabled = false;
    }

    public void SetActive()
    {
        preinteractionCanvas.enabled = true;
    }

    public void SetUnactive()
    {
        preinteractionCanvas.enabled = false;
    }
}
