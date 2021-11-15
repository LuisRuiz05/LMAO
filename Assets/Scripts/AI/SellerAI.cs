using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellerAI : MonoBehaviour
{
    public Text text;
    public DayNightCycle dayNight;
    public GameObject active;
    public ItemWorldSpawner itemSpawner;
    bool canSell = true;
    bool isTutorial;
    GameObject tutorial;
    Tutorial tutorialScript;
    Text money;
    SoundFXManager soundFX;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("SellerText").GetComponent<Text>();
        money = GameObject.Find("MoneyChange").GetComponent<Text>();
        soundFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundFXManager>();
        dayNight = GameObject.Find("Day/Night Cycle").GetComponent<DayNightCycle>();
        text.enabled = false;

        tutorial = GameObject.Find("Tutorial");
        if (tutorial != null)
        {
            tutorialScript = tutorial.GetComponent<Tutorial>();
            isTutorial = true;
        }
        else
        {
            isTutorial = false;
        }
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
            PlayerHandler player = other.GetComponent<PlayerHandler>();
            text.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isTutorial)
                {
                    tutorialScript.buySomething = true;
                    player.money = 0;
                }
                else
                {
                    if (player.money >= 25)
                    {
                        soundFX.source.PlayOneShot(soundFX.money);
                        money.color = Color.red;
                        money.text = "-$25";
                        StartCoroutine(ResetText());
                        player.money -= 25;
                        player.xp += 10;
                        itemSpawner.SpawnRandomItem();
                    }
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

    IEnumerator ResetText()
    {
        yield return new WaitForSecondsRealtime(2);
        money.text = "";
    }
}
