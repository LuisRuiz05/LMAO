using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    PlayerHandler player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
    }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        //Debug.Log(position);
        //Debug.Log(item.amount);
        //Debug.Log(item.itemType);
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        ItemWorld itemWorld = SpawnItemWorld(dropPosition, item);
        itemWorld.GetComponent<Rigidbody>().AddForce(0,0,2.0f, ForceMode.Impulse);
        return itemWorld;
    }

    private Item item;

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.inventory.AddItem(this.GetItem());
            DestroySelf();
        }
    }
}
