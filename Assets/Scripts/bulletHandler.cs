using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHandler : MonoBehaviour
{
    PlayerHandler player;
    public bool isEnemy;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
        StartCoroutine(WaitForDissapear());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && isEnemy)
        {
            player.health -= 30;
            player.blood.Play();
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy") && !isEnemy)
        {
            other.gameObject.GetComponent<EnemyAI>().npcHealth -= 100;
            Destroy(this.gameObject);
        }
        if(other.gameObject.CompareTag("EnemyMonster") && !isEnemy)
        {
            other.gameObject.GetComponent<EnemyCubeAI>().npcHealth -= 30;
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator WaitForDissapear()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(this.gameObject);
    }
}
