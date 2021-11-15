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
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("EnemyMonster"))
        {
            Debug.Log("Hit!");
            DragonAI dragon;
            collision.gameObject.TryGetComponent<DragonAI>(out dragon);
            if(dragon != null)
            {
                dragon.health -= 50;
            }
        }
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSecondsRealtime(8);
        Destroy(gameObject);
    }
}
