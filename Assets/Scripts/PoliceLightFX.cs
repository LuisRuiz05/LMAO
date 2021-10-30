using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class PoliceLightFX : MonoBehaviour
{
    public PoliceSpawner policeSpawner;
    public PostProcessVolume fx;
    ColorGrading color;
    Bloom bloom;
    public PlayerHandler player;

    bool toRed = true;
    bool justStarted = true;

    int maxSaturation = 0;

    void Start()
    {
        fx.enabled = true;
        fx.profile.TryGetSettings(out color);
        fx.profile.TryGetSettings(out bloom);
        bloom.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        AnxietySaturation();
        if (policeSpawner.inPersecution)
        {
            PatrolLights();
        }
        else
        {
            color.mixerBlueOutBlueIn.value = 100;
            color.mixerRedOutRedIn.value = 100;
            justStarted = true;
        }
    }

    void PatrolLights()
    {
        if (justStarted)
        {
            color.mixerBlueOutBlueIn.value = 200;
            color.mixerRedOutRedIn.value = 100;
            justStarted = false;
        }
        else
        {
            if (toRed)
            {
                if (color.mixerRedOutRedIn.value >= 200)
                {
                    color.mixerBlueOutBlueIn.value = 100;
                    color.mixerRedOutRedIn.value = 200;
                    toRed = false;
                } else
                {
                    color.mixerBlueOutBlueIn.value -= 1;
                    color.mixerRedOutRedIn.value += 1;
                }
            } else
            {
                if (color.mixerBlueOutBlueIn.value >= 200)
                {
                    color.mixerBlueOutBlueIn.value = 200;
                    color.mixerRedOutRedIn.value = 100;
                    toRed = true;
                }
                else
                {
                    color.mixerBlueOutBlueIn.value += 1;
                    color.mixerRedOutRedIn.value -= 1;
                }
            }
        }
    }

    void AnxietySaturation()
    {
        color.saturation.value = maxSaturation - player.anxiety;
        if(player.anxiety >= 100)
        {
            bloom.active = true;
        }
        else
        {
            bloom.active = false;
        }
    }
}
