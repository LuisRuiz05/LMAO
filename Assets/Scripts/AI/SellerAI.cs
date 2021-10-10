using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellerAI : MonoBehaviour
{
    public Text text;
    public DayNightCycle dayNight;
    PlayerHandler player;
    public GameObject active;
    public ItemWorldSpawner itemSpawner;
    bool canSell = true;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("SellerText").GetComponent<Text>();
        dayNight = GameObject.Find("Day/Night Cycle").GetComponent<DayNightCycle>();
        text.enabled = false;
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime();        
    }

    void CheckTime()
    {
        if (dayNight.dayNight == "day")
        {
            canSell = true;
            active.SetActive(true);
        }
        else
        {
            canSell = false;
            active.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canSell)
        {
            text.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (player.money >= 25)
                {
                    player.money -= 25;
                    itemSpawner.SpawnRandomItem();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.enabled = false;
        }
    }
}
