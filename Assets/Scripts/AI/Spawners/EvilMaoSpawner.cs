using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMaoSpawner : MonoBehaviour
{
    PlayerHandler player;
    public GameObject evilMao;
    bool hasSpawned = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.level >= 20 && !hasSpawned)
        {
            Instantiate(evilMao, transform);
            hasSpawned = true;
        }
    }
}
