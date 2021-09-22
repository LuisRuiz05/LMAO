using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PoliceIA : MonoBehaviour
{
    //public NavMeshAgent nav;
    int npcHealth = 100;
    public int action;
    public float cronometer;
    Animator animator;
    public Quaternion angle;
    public float range;

    public PlayerHandler player;
    public Rigidbody rb;
    public GameObject fxPoint;
    public GameObject fx;

    int playerSearch = 0;
    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isAlive)
        {

        }
    }

    void CheckAlive(int health)
    {
        if (health <= 0)
        {
            isAlive = false;
            animator.enabled = false;

            StartCoroutine(WaitForDissapear());
        }
    }

    IEnumerator WaitForDissapear()
    {
        yield return new WaitForSecondsRealtime(4);
        Destroy(this.gameObject);
    }
}
