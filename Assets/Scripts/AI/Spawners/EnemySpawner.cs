using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    PlayerHandler player;
    public Transform[] masterSpawner;
    float intoxication;
    public GameObject enemy;
    public GameObject cyclopeCube;
    public GameObject devilCube;
    public GameObject dragon;
    bool isSpawning = false;

    public List<GameObject> enemyList = new List<GameObject>();

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
        CleanList();
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
                foreach (Transform childrenInChildrenXD in children)
                {
                    Destroy(childrenInChildrenXD.gameObject);
                    enemyList.Clear();
                }
            }
        }
    }

    void CleanList()
    {
        foreach(GameObject enemy in enemyList.ToArray())
        {
            if (enemy == null)
            {
                enemyList.Remove(enemy);
            }
        }
    }

    GameObject DecideEnemy()
    {
        int randomEnemy = Random.Range(0, 10);
        // 0-5
        if (player.level <= 5)
        {
            return enemy;
        }
        // 6-10
        if (player.level > 5 && player.level <= 10)
        {
            if(randomEnemy < 9)
            {
                return enemy;
            } else
            {
                return cyclopeCube;
            }
        }
        // 11 - 15
        if (player.level > 10 && player.level <= 15)
        {
            if (randomEnemy < 6)
            {
                return enemy;
            }
            if (randomEnemy == 6 || randomEnemy == 7 || randomEnemy == 8)
            {
                return cyclopeCube;
            }
            if (randomEnemy == 9)
            {
                return devilCube;
            }
            else
            {
                return dragon;
            }
        }
        // > 16
        else
        {
            if (randomEnemy < 5)
            {
                return enemy;
            }
            if (randomEnemy >= 5 && randomEnemy < 8)
            {
                return cyclopeCube;
            }
            if (randomEnemy == 8 || randomEnemy == 10)
            {
                return devilCube;
            }
            if (randomEnemy == 9)
            {
                return dragon;
            }
            else
            {
                return enemy;
            }
        }
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSecondsRealtime(5);
        int randomSpawn = Random.Range(0, masterSpawner.Length);
        GameObject enemyClone = Instantiate(DecideEnemy(), masterSpawner[randomSpawn]);
        enemyList.Add(enemyClone);
        isSpawning = false;
    }

}
