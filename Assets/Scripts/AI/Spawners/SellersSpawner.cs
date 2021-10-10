using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellersSpawner : MonoBehaviour
{
    public GameObject seller;
    public Transform[] masterSpawner;

    // Start is called before the first frame update
    void Start()
    {
        SpawnSellers();
    }

    void SpawnSellers()
    {
        foreach (Transform spawn in masterSpawner)
        {
            GameObject sellerClone = Instantiate(seller, spawn);
        }
    }
}
