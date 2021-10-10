using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public Transform tangamadapio;
    public List<Light> lightsInCity = new List<Light>();
    public DayNightCycle dayNight;
    bool lightsOn = true;

    void Start()
    {
        foreach(Transform child in tangamadapio.transform)
        {
            Light light = child.GetComponentInChildren<Light>();
            if (light != null)
            {
                lightsInCity.Add(light);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeLights();
    }

    void ChangeLights()
    {
        if(dayNight.dayNight == "day")
        {
            if (lightsOn)
            {
                foreach(Light light in lightsInCity)
                {
                    light.enabled = false;
                    lightsOn = false;
                }
            }
        } else
        {
            if (!lightsOn)
            {
                foreach (Light light in lightsInCity)
                {
                    light.enabled = true;
                    lightsOn = true;
                }
            }
        }
    }
}
