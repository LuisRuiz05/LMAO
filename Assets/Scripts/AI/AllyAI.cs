using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAI : MonoBehaviour
{
    PlayerHandler player;
    public NavMeshAgent nav;
    int npcHealth = 100;
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
    bool canShoot = true;
    bool isChasingEnemy = false;
    List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyList = GameObject.Find("EnemyGangSpawner").GetComponent<EnemySpawner>().enemyList;
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAlive();
        FollowPlayer();
        DecideClosestEnemy();
    }

    void FollowPlayer()
    {
        if (!isChasingEnemy)
        {
            Vector3 playerPosition = GameObject.Find("Player").transform.position;

            nav.SetDestination(playerPosition);
            nav.speed = 3.5f;
            nav.stoppingDistance = 3f;
            if (nav.velocity.magnitude <= 0)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
        }
    }

    void CheckAlive()
    {
        if (!player.isDrunk)
        {
            animator.Play("Die");
            nav.enabled = false;

            StartCoroutine(WaitForDissapear());
        }
    }

    void DecideClosestEnemy()
    {
        float distanceToClosestEnemy = 10000f;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                if (distanceToEnemy < 15f && distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }
        if (closestEnemy != null) {
            ChaseEnemy(closestEnemy, distanceToClosestEnemy);
        }
    }

    void ChaseEnemy(GameObject enemy, float distance)
    {
        if (enemy != null && enemy.GetComponent<EnemyAI>().isAlive)
        {
            isChasing = true;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
            nav.enabled = true;
            nav.SetDestination(enemy.transform.position);
            nav.speed = 4.2f;
            nav.stoppingDistance = 0.5f;
            if (distance <= 20f)
            {
                AttackEnemy();
            }
        }
    }

    void AttackEnemy()
    {
        if (canShoot)
        {
            animator.Play("Shoot");
            GameObject bulletClone = Instantiate(bullet, bulletTransform);
            Rigidbody rbBulletClone = bulletClone.AddComponent<Rigidbody>();
            rbBulletClone.AddForce(transform.forward * 60f, ForceMode.Impulse);
            canShoot = false;
            StartCoroutine(WaitForShoot());
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
