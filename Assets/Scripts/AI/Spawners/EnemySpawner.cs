using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    PlayerHandler player;
    float intoxication;
    public GameObject enemy;
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
                Destroy(children.gameObject);
                enemyList.Clear();
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

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSecondsRealtime(5);
        GameObject enemyClone = Instantiate(enemy, gameObject.transform);
        enemyList.Add(enemyClone);
        isSpawning = false;
    }

}
