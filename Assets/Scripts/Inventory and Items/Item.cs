using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Food,
        Water,
        Beer,
        Drugs,
        Gun,
        Medkit
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Food: return ItemAssets.Instance.foodSprite;
            case ItemType.Water: return ItemAssets.Instance.waterSprite;
            case ItemType.Beer: return ItemAssets.Instance.beerSprite;
            case ItemType.Drugs: return ItemAssets.Instance.drugsSprite;
            case ItemType.Gun: return ItemAssets.Instance.gunSprite;
            case ItemType.Medkit: return ItemAssets.Instance.medkitSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Food:
            case ItemType.Water:
            case ItemType.Beer:
            case ItemType.Drugs:
            case ItemType.Medkit:
                return true;
            case ItemType.Gun:
                return false;
        }
    }
}
