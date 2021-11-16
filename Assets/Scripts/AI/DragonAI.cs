using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonAI : MonoBehaviour
{
    public ParticleSystem fireHorizontal;
    public ParticleSystem fireEmbers;
    public ParticleSystem smokeCampfire;
    public ParticleSystem smokeMedium;
    [SerializeField]
    Collider fireCollider;

    NavMeshAgent nav;
    PlayerHandler playerHandler;
    GameObject player;
    SoundFXManager soundFX;

    bool canFire = true;
    bool isAlive = true;

    public int health = 100;
    public int action;
    public float cronometer;
    public Quaternion angle;
    public float range;

    public GameObject fxPoint;
    public GameObject fx;

    void Start()
    {
        fireCollider = GetComponentInChildren<Collider>();
        nav = GetComponent<NavMeshAgent>();
        nav.stoppingDistance = 6f;
        player = GameObject.Find("Player");
        playerHandler = player.GetComponent<PlayerHandler>();
        soundFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundFXManager>();
    }

    void Update()
    {
        CheckAlive(health);
        isLookingPlayer();
        Fire();
    }

    void CheckAlive(int health)
    {
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            playerHandler.xp += 50;
            createFX();
            soundFX.source.PlayOneShot(soundFX.dragonDeath);
            Destroy(gameObject);
        }
    }

    void createFX()
    {
        GameObject createdFX = Instantiate(fx, fxPoint.transform.position, fxPoint.transform.rotation);
        Destroy(createdFX, 2f);
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
                    break;
                case 1:
                    range = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, range, 0);
                    action++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
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
        nav.enabled = true;
        nav.SetDestination(playerPosition);
    }

    void Fire()
    {
        if (canFire)
        {
            soundFX.source.PlayOneShot(soundFX.dragonFire);
            fireHorizontal.Play();
            fireEmbers.Play();
            smokeCampfire.Play();
            smokeMedium.Play();

            StartCoroutine(WaitForFire());
            canFire = false;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (fireHorizontal.isPlaying) {
                playerHandler.health -= 7f * Time.deltaTime;
            }
        }
    }

    IEnumerator WaitForFire()
    {
        yield return new WaitForSecondsRealtime(10);
        canFire = true;
    }
}
