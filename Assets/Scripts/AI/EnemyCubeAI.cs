using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCubeAI : MonoBehaviour
{
    GameObject player;
    PlayerHandler playerHandler;
    public NavMeshAgent nav;

    public int npcHealth = 100;
    public int action;
    public float cronometer;
    Animator animator;
    public Quaternion angle;
    public float range;

    public GameObject fxPoint;
    public GameObject fx;

    public bool isAlive = true;
    bool canHit = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHandler = player.GetComponent<PlayerHandler>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isLookingPlayer();
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

    void isLookingPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        float distance = Vector3.Distance(playerPosition, transform.position);

        if (distance <= 15.0f)
        {
            ChasePlayer(playerPosition);
        }
        else
        {
            PeopleBehaviour();
        }
    }

    void ChasePlayer(Vector3 playerPosition)
    {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
            nav.enabled = true;
            nav.SetDestination(playerPosition);
            nav.speed = 8;
    }

    void CheckAlive(int health)
    {
        if (health <= 0)
        {
            isAlive = false;
            animator.SetTrigger("Die");
            nav.enabled = false;
            StartCoroutine(WaitForDissapear());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canHit)
            {
                animator.SetTrigger("Attack1");
                playerHandler.health -= 30;
                canHit = false;
                StartCoroutine(WaitForHit());
            }
        }
    }

    void createFX()
    {
        GameObject createdFX = Instantiate(fx, fxPoint.transform.position, fxPoint.transform.rotation);
        Destroy(createdFX, 2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                createFX();
                npcHealth -= 50;
            }
        }
    }

    IEnumerator WaitForDissapear()
    {
        nav.enabled = false;
        yield return new WaitForSecondsRealtime((float)1.8);
        Destroy(this.gameObject);
        playerHandler.xp += 50;
    }

    IEnumerator WaitForHit()
    {
        yield return new WaitForSeconds(2);
        animator.ResetTrigger("Attack1");
        canHit = true;
    }

}
