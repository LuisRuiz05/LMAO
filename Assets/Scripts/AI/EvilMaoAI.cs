using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EvilMaoAI : MonoBehaviour
{
    GameObject player;
    PlayerHandler playerHandler;
    NavMeshAgent nav;
    Animator animator;

    public Image healthImage;
    SoundFXManager soundFX;

    public int health = 1000;
    int maxHealth = 1000;
    bool isAlive = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHandler = player.GetComponent<PlayerHandler>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        soundFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        UpdateLifeBar();
        CheckAlive(health);
    }

    void CheckAlive(int health)
    {
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            soundFX.source.PlayOneShot(soundFX.cubesDeath);
            animator.SetBool("Walk", false);
            animator.SetBool("Hit", false);
            animator.Play("Die");
            nav.speed = 0;
            nav.acceleration = 0;
            nav.enabled = false;
            StartCoroutine(WaitForDissapear());
        }
    }

    void UpdateLifeBar()
    {
        healthImage.fillAmount = (float)health / maxHealth;
    }

    void FollowPlayer()
    {
        nav.SetDestination(player.transform.position);
        animator.SetBool("Walk", true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Hit", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Hit", false);
        }
    }

    IEnumerator WaitForDissapear()
    {
        nav.enabled = false;
        soundFX.source.PlayOneShot(soundFX.playersDeath);
        yield return new WaitForSecondsRealtime(3);
        Destroy(this.gameObject);
        playerHandler.xp += 50000000;
    }
}
