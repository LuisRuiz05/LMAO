using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;
    public bool isRandom = false;

    private void Start()
    {
        if (!isRandom) {
            ItemWorld.SpawnItemWorld(transform.position, item);
            Destroy(gameObject);
        }
    }

    public void SpawnRandomItem()
    {
        int randomInt = Random.Range(1, 10);
        switch (randomInt)
        {
            default:
            case 1: item = new Item { itemType = Item.ItemType.Food, amount = 2 }; break;
            case 2: item = new Item { itemType = Item.ItemType.Food, amount = 1 }; break;
            case 3: item = new Item { itemType = Item.ItemType.Food, amount = 1 }; break;
            case 4: item = new Item { itemType = Item.ItemType.Food, amount = 1 }; break;
            case 5: item = new Item { itemType = Item.ItemType.Water, amount = 1 }; break;
            case 6: item = new Item { itemType = Item.ItemType.Water, amount = 1 }; break;
            case 7: item = new Item { itemType = Item.ItemType.Water, amount = 1 }; break;
            case 8: item = new Item { itemType = Item.ItemType.Medkit, amount = 1 }; break;
            case 9: item = new Item { itemType = Item.ItemType.Medkit, amount = 1 }; break;
            case 10: item = new Item { itemType = Item.ItemType.Beer, amount = 1 }; break;
        }
        ItemWorld.SpawnItemWorld(transform.position, item);
    }

    public void SpawnRandomDrugItem()
    {
        int randomInt = Random.Range(1, 5);
        switch (randomInt)
        {
            default:
            case 1: item = new Item { itemType = Item.ItemType.Drugs, amount = 1 }; break;
            case 2: item = new Item { itemType = Item.ItemType.Drugs, amount = 1 }; break;
            case 3: item = new Item { itemType = Item.ItemType.Drugs, amount = 1 }; break;
            case 4: item = new Item { itemType = Item.ItemType.Beer, amount = 2 }; break;
            case 5: item = new Item { itemType = Item.ItemType.Beer, amount = 1 }; break;
        }
        ItemWorld.SpawnItemWorld(transform.position, item);
    }
}
