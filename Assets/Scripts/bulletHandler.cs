using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHandler : MonoBehaviour
{
    PlayerHandler player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
        StartCoroutine(WaitForDissapear());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.health -= 30;
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
