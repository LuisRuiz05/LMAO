using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PeopleIA : MonoBehaviour
{
    public int npcHealth = 100;
    public int action;
    public float cronometer;
    Animator animator;
    public Quaternion angle;
    public float range;
    public Preinteraction preinteraction;
    PlayerHandler player;
    public Rigidbody rb;
    public GameObject fxPoint;
    public GameObject fx;

    Text money;
    SoundFXManager soundFX;

    public GameObject availableBall;
    public GameObject givenBall;
    public GameObject stolenBall;

    bool hasStolen = false;
    bool hasGiven = false;
    bool isAlive = true;

    //Head
    public GameObject npcHead;
    //Body
    public GameObject npcTop;
    //Bottom
    public GameObject npcBottom;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        preinteraction = GameObject.Find("Preinteraction").GetComponent<Preinteraction>();
        money = GameObject.Find("MoneyChange").GetComponent<Text>();
        soundFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundFXManager>();
        animator = GetComponent<Animator>();
        preinteraction.SetUnactive();
        UpdateNPCState();
    }

    // Update is called once per frame
    void Update()
    {
        PeopleBehaviour();
        CheckAlive(npcHealth);
        UpdateNPCState();
    }

    public void SetSkin(Material head, Material top, Material bottom)
    {
        npcHead.GetComponent<SkinnedMeshRenderer>().material = head;
        npcTop.GetComponent<SkinnedMeshRenderer>().material = top;
        npcBottom.GetComponent<SkinnedMeshRenderer>().material = bottom;
    }

    public void PeopleBehaviour()
    {
        if (isAlive)
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
        }
    }

    void createFX()
    {
        GameObject createdFX = Instantiate(fx, fxPoint.transform.position,fxPoint.transform.rotation);
        Destroy(createdFX, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            preinteraction.SetActive();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.search += Random.Range(4,13);
                npcHealth -= 25;
                createFX();
                if (npcHealth > 0)
                {
                    rb.AddForce(transform.up * 650f);
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && isAlive && !hasStolen)
            {
                int given = Random.Range(0, 50);
                soundFX.source.PlayOneShot(soundFX.money);
                money.color = Color.green;
                money.text = "+$"+given;
                StartCoroutine(ResetText());
                player.money += given;
                player.search += Random.Range(15,25);
                hasStolen = true;
                if (given > 0)
                {
                    player.xp += 14;
                }
                StartCoroutine(WaitForSteal());
            }
            if (Input.GetKeyDown(KeyCode.R) && isAlive)
            {
                if (!hasGiven && !hasStolen)
                {
                    int given = Random.Range(0, 4);
                    soundFX.source.PlayOneShot(soundFX.money);
                    money.color = Color.green;
                    money.text = "+$" + given;
                    StartCoroutine(ResetText());
                    player.money += given;
                    if (given > 0)
                    {
                        player.xp += 6;
                    }
                    hasGiven = true;
                    StartCoroutine(WaitForGive());
                } else
                {
                    player.search += Random.Range(1, 7);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isAlive)
        {
            preinteraction.SetUnactive();
        }
    }

    void CheckAlive(int health)
    {
        if (health <= 0)
        {
            isAlive = false;
            animator.Play("Die");
            preinteraction.SetUnactive();
            StartCoroutine(WaitForDissapear());
        }
    }

    void UpdateNPCState()
    {
        if(!hasGiven && !hasStolen)
        {
            availableBall.SetActive(true);
            givenBall.SetActive(false);
            stolenBall.SetActive(false);
        } else if(hasGiven && !hasStolen)
        {
            availableBall.SetActive(false);
            givenBall.SetActive(true);
            stolenBall.SetActive(false);
        } else if (hasStolen)
        {
            availableBall.SetActive(false);
            givenBall.SetActive(false);
            stolenBall.SetActive(true);
        }
    }

    IEnumerator ResetText()
    {
        yield return new WaitForSecondsRealtime(2);
        money.text = "";
    }

    IEnumerator WaitForGive()
    {
        yield return new WaitForSecondsRealtime(300);
        hasGiven = false;
    }

    IEnumerator WaitForSteal()
    {
        yield return new WaitForSecondsRealtime(600);
        hasStolen = false;
    }

    IEnumerator WaitForDissapear()
    {
        yield return new WaitForSecondsRealtime((float)1.8);
        Destroy(this.gameObject);
    }
}
