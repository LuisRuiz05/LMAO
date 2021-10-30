using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnProgressionChange : MonoBehaviour
{
    public PlayerHandler player;
    public PoliceIA police;
    public EnemyAI enemy;

    int level;

    void Start()
    {
        level = player.level;
    }

    // Update is called once per frame
    void Update()
    {
        GetUpdatedPlayerLevel();
        MoreEnemies();
    }

    void GetUpdatedPlayerLevel()
    {
        level = player.level;
    }

    void MoreEnemies()
    {

    }
}
