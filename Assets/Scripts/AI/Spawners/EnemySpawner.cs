using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    PlayerHandler player;
    float intoxication;
    public GameObject enemy;
    bool isSpawning = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        intoxication = player.intoxication;
        SpawnEnemies();
        DespawnEnemies();
    }

    void SpawnEnemies()
    {
        if(intoxication >= 1 && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(WaitForSpawn());
        }
    }

    void DespawnEnemies()
    {
        if (intoxication == 0)
        {
            foreach (Transform children in transform)
            {
                Destroy(children.gameObject);
            }
        }
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSecondsRealtime(10);
        GameObject enemyClone = Instantiate(enemy, gameObject.transform);
        isSpawning = false;
    }

}
