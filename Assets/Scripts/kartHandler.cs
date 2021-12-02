using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kartHandler : MonoBehaviour
{
    //Collider collider;

    private void Start()
    {
        //collider = GetComponent<Collider>();
        StartCoroutine(WaitForDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyMonster"))
        {
            DragonAI dragon;

            collision.gameObject.TryGetComponent<DragonAI>(out dragon);

            if (dragon != null)
            {
                dragon.health -= 50;
            }
        }
        if (collision.gameObject.CompareTag("EvilMao"))
        {
            EvilMaoAI evilMao;

            collision.gameObject.TryGetComponent<EvilMaoAI>(out evilMao);

            if (evilMao != null)
            {
                evilMao.health -= 50;
            }
        }
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSecondsRealtime(8);
        Destroy(gameObject);
    }
}
