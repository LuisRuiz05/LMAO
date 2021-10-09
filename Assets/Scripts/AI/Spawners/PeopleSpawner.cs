using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject npc;
    public Transform[] masterSpawner;

    //Materials
    //Head
    public Material head01;
    public Material head02;
    public Material head03;
    public Material head04;
    public Material head05;
    public Material head06;

    //Top
    public Material top01;
    public Material top02;
    public Material top03;

    //Bottom
    public Material bottom01;
    public Material bottom02;
    public Material bottom03;

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
        Material chosenHead;
        Material chosenTop;
        Material chosenBottom;

        int randomHead = Random.Range(1, 6);
        int randomTop = Random.Range(1, 3);
        int randomBottom = Random.Range(1, 3);

        switch (randomHead)
        {
            default:
            case 1: chosenHead = head01; break;
            case 2: chosenHead = head02; break;
            case 3: chosenHead = head03; break;
            case 4: chosenHead = head04; break;
            case 5: chosenHead = head05; break;
            case 6: chosenHead = head06; break;
        }

        switch (randomTop)
        {
            default:
            case 1: chosenTop = top01; break;
            case 2: chosenTop = top02; break;
            case 3: chosenTop = top03; break;
        }

        switch (randomBottom)
        {
            default:
            case 1: chosenBottom = bottom01; break;
            case 2: chosenBottom = bottom02; break;
            case 3: chosenBottom = bottom03; break;
        }

        GameObject npcClone = Instantiate(npc, spawn);
        npcClone.GetComponent<PeopleIA>().SetSkin(chosenHead, chosenTop, chosenBottom);
    }
}
