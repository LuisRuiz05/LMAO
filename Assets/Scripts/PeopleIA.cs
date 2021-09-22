using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PeopleIA : MonoBehaviour
{
    //public NavMeshAgent nav;
    int npcHealth = 100;
    public int action;
    public float cronometer;
    Animator animator;
    public Quaternion angle;
    public float range;
    public GameObject preinteraction;
    public PlayerHandler player;
    public Rigidbody rb;
    public GameObject fxPoint;
    public GameObject fx;

    bool hasStolen = false;
    bool hasGiven = false;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        preinteraction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PeopleBehaviour();
        CheckAlive(npcHealth);
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
            preinteraction.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.search += Random.Range(4,13);
                rb.AddForce(transform.up * 650f);
                npcHealth -= 25;
                createFX();
            }
            if (Input.GetKeyDown(KeyCode.E) && isAlive && !hasStolen)
            {
                player.money += Random.Range(0, 50);
                player.search += Random.Range(15,25);
                hasStolen = true;
                StartCoroutine(WaitForSteal());
            }
            if (Input.GetKeyDown(KeyCode.R) && isAlive)
            {
                if (!hasGiven)
                {
                    player.money += Random.Range(0,4);
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
            Debug.Log("Touching Player");
            preinteraction.SetActive(false);
        }
    }

    void CheckAlive(int health)
    {
        if (health <= 0)
        {
            isAlive = false;
            animator.enabled = false;
            preinteraction.SetActive(false);
            StartCoroutine(WaitForDissapear());
        }
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
        yield return new WaitForSecondsRealtime(4);
        Destroy(this.gameObject);
    }
}
