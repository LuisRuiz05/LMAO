using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCubeAI : MonoBehaviour
{
    GameObject player;
    PlayerHandler playerHandler;
    public NavMeshAgent nav;
    public GameObject mini;
    public enum enemyType
    {
        Cyclope,
        Devil,
        MiniDevil
    }
    public enemyType type;

    public int npcHealth = 100;
    public int action;
    public float cronometer;
    Animator animator;
    public Quaternion angle;
    public float range;
    SoundFXManager soundFX;

    public GameObject fxPoint;
    public GameObject fx;

    public bool isAlive = true;
    bool canHit = true;
    bool hitDelay = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHandler = player.GetComponent<PlayerHandler>();
        animator = GetComponent<Animator>();
        soundFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundFXManager>();
        if (type == enemyType.MiniDevil)
        {
            npcHealth = 50;
        }
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
        if (isAlive)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
            nav.enabled = true;
            nav.SetDestination(playerPosition);
            nav.speed = 8;
        }
    }

    void CheckAlive(int health)
    {
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            soundFX.source.PlayOneShot(soundFX.cubesDeath);
            animator.SetTrigger("Die");
            nav.speed = 0;
            nav.acceleration = 0;
            nav.enabled = false;
            StartCoroutine(WaitForDissapear());
        }
    }

    /*void OnCollisionStay(Collision collision)
    {
        if (hitDelay)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (canHit)
                {
                    animator.SetTrigger("Attack1");
                    if (type != enemyType.MiniDevil)
                    {
                        playerHandler.health -= 35;
                    }
                    else
                    {
                        playerHandler.health -= 15;

                    }
                    canHit = false;
                    StartCoroutine(WaitForHit());
                }
            }
            hitDelay = false;
        }
        else
        {
            StartCoroutine(AttackDelay());
        }
    }*/

    void createFX()
    {
        GameObject createdFX = Instantiate(fx, fxPoint.transform.position, fxPoint.transform.rotation);
        Destroy(createdFX, 2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Get hurt
            if (Input.GetKeyDown(KeyCode.Q))
            {
                createFX();
                npcHealth -= 50;
            }
            //Hit player
            if (hitDelay)
            {
                if (canHit)
                {
                    animator.SetTrigger("Attack1");
                    soundFX.source.PlayOneShot(soundFX.cubesAttack);
                    if (type != enemyType.MiniDevil)
                    {
                        playerHandler.health -= 35;
                    }
                    else
                    {
                        playerHandler.health -= 15;
                    }
                    canHit = false;
                    StartCoroutine(WaitForHit());
                }
                hitDelay = false;
            }
            else
            {
                StartCoroutine(AttackDelay());
            }
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSecondsRealtime(3);
        hitDelay = true;
    }

    IEnumerator WaitForDissapear()
    {
        nav.enabled = false;
        yield return new WaitForSecondsRealtime((float)1.8);
        if (type == enemyType.Devil)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject miniDevil = Instantiate(mini, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            }
        }
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
