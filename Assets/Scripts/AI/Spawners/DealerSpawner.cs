using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerSpawner : MonoBehaviour
{
    public GameObject dealer;
    public Transform[] masterSpawner;
    public DayNightCycle dayNight;
    bool canSpawn = false;
    bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        dayNight = GameObject.Find("Day/Night Cycle").GetComponent<DayNightCycle>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime();
    }

    void CheckTime()
    {
        if (dayNight.dayNight == "night")
        {
            canSpawn = true;
            if (canSpawn && !hasSpawned)
            {
                SpawnDealers();
            }
        }
        else
        {
            canSpawn = false;
            hasSpawned = false;
        }
    }


    void SpawnDealers()
    {
        foreach (Transform spawn in masterSpawner)
        {
            GameObject dealerClone = Instantiate(dealer, spawn);
            hasSpawned = true;
        }
    }

}
