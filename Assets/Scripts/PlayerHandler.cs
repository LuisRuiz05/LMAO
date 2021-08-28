using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHandler : MonoBehaviour
{
    public PostProcessVolume drunkFX;
    public bool isDrunk = false;

    // Update is called once per frame
    void Update()
    {
        if (isDrunk)
        {
            drunkFX.enabled = true;
        }
        else
        {
            drunkFX.enabled = false;
        }
    }
}
