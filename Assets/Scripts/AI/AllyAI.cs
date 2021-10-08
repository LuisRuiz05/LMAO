using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAI : MonoBehaviour
{
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
    bool isAlive = true;
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
        CheckAlive(npcHealth);
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        nav.enabled = true;
        
        nav.SetDestination(playerPosition);
        if (nav.velocity.magnitude <= 0){
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
        }
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
