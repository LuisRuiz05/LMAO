using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;
    public Skybox sky;

    public Material daySky;
    public Material noonSky;
    public Material nightSky;

    float timeDelay = 0.02f;
    float maxIntensity = 2;
    float minIntensity = 0;
    float intensity;

    bool toNight = true;

    // Start is called before the first frame update
    void Start()
    {
        intensity = sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLight();
        UpdateSkybox();
    }

    void UpdateLight()
    {
        if (toNight)
        {
            intensity -= Time.deltaTime * timeDelay;
            if (intensity <= minIntensity)
            {
                toNight = false;
            }
        }
        else
        {
            intensity += Time.deltaTime * timeDelay;
            if (intensity >= maxIntensity)
            {
                toNight = true;
            }
        }

        sun.intensity = intensity;
    }

    void UpdateSkybox()
    {
        if (intensity <= 0.35f)
        {
            sky.material = nightSky;
            sun.shadows = LightShadows.None;
        } else if (intensity > 0.35f && intensity <= 0.65f)
        {
            sky.material = noonSky;
            sun.shadows = LightShadows.Hard;
        }
        else
        {
            sky.material = daySky;
            sun.shadows = LightShadows.Hard;
        }
    }
}
