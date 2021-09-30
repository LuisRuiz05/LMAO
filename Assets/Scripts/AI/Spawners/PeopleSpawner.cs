using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject npc;
    public Transform[] masterSpawner;

    void Start()
    {
        foreach (Transform transform in masterSpawner) {
            Spawn(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(Transform spawn)
    {
        GameObject npcClone = Instantiate(npc, spawn);
    }
}
