using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    public GameObject policePrefab;
    public Transform[] masterSpawner;
    public PlayerHandler player;
    int minimumRequired = 4;
    public int policesRequired;
    public List<GameObject> policeList = new List<GameObject>();
    bool isChasing = false;
    public bool inPersecution = false;
    int maxPolicesInSearch = 0;

    // Update is called once per frame
    private void Start()
    {
        policesRequired = minimumRequired;
    }
    void Update()
    {
        isChasing = CheckChase();
        SpawnPolice();
        UpdateSearch();
        RespawnPolice();
        DespawnPolice();
    }

    void SpawnPolice()
    {
        if(player.search < 50) {
            policesRequired = minimumRequired;
            maxPolicesInSearch = 0;
        }

        if (player.search >= 50) {
            if (((int)player.search / 50) > maxPolicesInSearch)
            {
                inPersecution = true;
                maxPolicesInSearch = ((int)player.search / 50);
            }
            policesRequired = minimumRequired + maxPolicesInSearch;
        }

        if (policeList.Count < policesRequired)
        {
            int randomSpawn = Random.Range(0, masterSpawner.Length);
            GameObject policeClone = Instantiate(policePrefab, masterSpawner[randomSpawn]);
            policeList.Add(policeClone);
        }
    }

    void UpdateSearch()
    {
        if (player.search > 0 && !isChasing)
        {
            if (player.search < 50) {
                player.search -= 0.5f * Time.deltaTime;
            }
            else if (player.search >= 50 && player.search < 225)
            {
                player.search -= 4f * Time.deltaTime;
            } else
            {
                player.search -= 7f * Time.deltaTime;
            }
        }
    }

    bool CheckChase()
    {
        foreach (GameObject police in policeList)
        {
            if (police != null)
            {
                if (police.GetComponent<PoliceIA>().isChasing)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void RespawnPolice()
    {
        foreach (GameObject police in policeList) {
            if (police == null)
            {
                StartCoroutine(WaitToRespawn(police));
            }
        }
    }

    void DespawnPolice()
    {
        if (policesRequired < policeList.Count)
        {
            if (inPersecution)
            {
                player.xp += 20;
            }
            inPersecution = false;
            Destroy(policeList[0]);
            policeList.RemoveAt(policeList.Count-1);
        }
    }

    IEnumerator WaitToRespawn(GameObject police)
    {
        yield return new WaitForSecondsRealtime(5);
        policeList.Remove(police);
    }
}
