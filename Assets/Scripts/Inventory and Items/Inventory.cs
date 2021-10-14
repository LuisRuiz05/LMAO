using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction, bool isTutorial) {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        if (!isTutorial)
        {
            AddItem(new Item { itemType = Item.ItemType.Food, amount = 3 });
            AddItem(new Item { itemType = Item.ItemType.Water, amount = 3 });
            AddItem(new Item { itemType = Item.ItemType.Beer, amount = 2 });
            AddItem(new Item { itemType = Item.ItemType.Medkit, amount = 1 });
            AddItem(new Item { itemType = Item.ItemType.Drugs, amount = 1 });
        }
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemInInventory = true;
                }
            }
            if (!itemInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
