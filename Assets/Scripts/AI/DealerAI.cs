using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerAI : MonoBehaviour
{
    public Text text;
    public DayNightCycle dayNight;
    public ItemWorldSpawner itemSpawner;
    bool canSell = true;
    bool canWalk = true;

    public int action;
    public float cronometer;
    public Animator animator;
    public Quaternion angle;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("SellerText").GetComponent<Text>();
        dayNight = GameObject.Find("Day/Night Cycle").GetComponent<DayNightCycle>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime();
        WalkAround();
    }

    void CheckTime()
    {
        if (dayNight.dayNight == "day")
        {
            Destroy(gameObject);
        }
    }

    void WalkAround()
    {
        if (canWalk)
        {
            cronometer += 1 * Time.deltaTime;
            if (cronometer >= 3)
            {
                action = Random.Range(0, 2);
                cronometer = 0;
            }
            switch (action)
            {
                case 0:
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    break;
                case 1:
                    range = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, range, 0);
                    action++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
                    break;
            }
        } else {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canSell)
        {
            PlayerHandler player = other.GetComponent<PlayerHandler>();
            text.enabled = true;
            canWalk = false;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (player.money >= 25)
                {
                    player.money -= 25;
                    player.xp += 10;
                    itemSpawner.SpawnRandomDrugItem();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canWalk = true;
            text.enabled = false;
        }
    }
}
