using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent nav;
    public int npcHealth = 100;
    public Transform bulletTransform;
    public GameObject bullet;
    public int action;
    public float cronometer;
    Animator animator;
    public Quaternion angle;
    public float range;

    public Rigidbody rb;
    public GameObject fxPoint;
    public GameObject fx;

    public bool isChasing = false;
    public bool isAlive = true;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nav.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        isLookingPlayer();
        CheckAlive(npcHealth);
    }

    void CheckAlive(int health)
    {
        if (health <= 0)
        {
            isAlive = false;
            animator.Play("Die");
            nav.enabled = false;

            StartCoroutine(WaitForDissapear());
        }
    }

    public void PeopleBehaviour()
    {
        if (isAlive)
        {
            isChasing = false;
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
        Vector3 forward = -transform.forward;
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Vector3 target = (playerPosition - transform.position).normalized;
        float distance = Vector3.Distance(playerPosition, transform.position);

        if ((Vector3.Dot(forward, target) < 0.2f && distance <= 30.0f) || distance <= 4f)
        {
            ChasePlayer(playerPosition, distance);
        }
        else
        {
            PeopleBehaviour();
        }
    }

    void ChasePlayer(Vector3 playerPosition, float distance)
    {
        isChasing = true;
        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
        nav.enabled = true;
        nav.SetDestination(playerPosition);
        nav.speed = 4.2f;
        if (distance <= 20f) {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (canShoot)
        {
            nav.speed = 0f;
            nav.acceleration = 0f;
            animator.Play("Shoot");
            GameObject bulletClone = Instantiate(bullet, bulletTransform);
            bulletClone.GetComponent<bulletHandler>().isEnemy = true;
            Rigidbody rbBulletClone = bulletClone.AddComponent<Rigidbody>();
            rbBulletClone.AddForce(transform.forward * 60f, ForceMode.Impulse);
            canShoot = false;
            nav.speed = 4.2f;
            nav.acceleration = 8f;
            StartCoroutine(WaitForShoot());
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
                if (npcHealth > 0)
                {
                    other.GetComponent<PlayerHandler>().xp += 30;
                }
                npcHealth -= 100;
            }
        }
    }

    IEnumerator WaitForDissapear()
    {
        yield return new WaitForSecondsRealtime((float)1.8);
        Destroy(this.gameObject);
    }

    IEnumerator WaitForShoot()
    {
        yield return new WaitForSecondsRealtime(2);
        canShoot = true;
    }
}
