using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    public GameObject policePrefab;
    Transform spawn;
    public PlayerHandler player;
    public int policesRequired = 0;
    public List<GameObject> policeList = new List<GameObject>();
    bool isChasing = false;

    // Update is called once per frame
    private void Start()
    {
        spawn = gameObject.transform;
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
        if(player.search < 0) {
            policesRequired = 0;
        }

        if (player.search > 0 && player.search < 100) {
            policesRequired = 1;
        } else {
            policesRequired = ((int)player.search / 50);
        }

        if (policeList.Count < policesRequired)
        {
            GameObject policeClone = Instantiate(policePrefab, spawn);
            policeList.Add(policeClone);
        }
    }

    void UpdateSearch()
    {
        if (player.search > 0 && !isChasing)
        {
            player.search -= 1 * Time.deltaTime;
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
            Destroy(policeList[0]);
            policeList.RemoveAt(policeList.Count-1);
        }
    }

    IEnumerator WaitToRespawn(GameObject police)
    {
        yield return new WaitForSecondsRealtime(10);
        policeList.Remove(police);
    }
}
