using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public int money = 0;
    float maxHealth = 100;
    public float health = 100;
    float maxFood = 100f;
    public float food = 75f;
    float maxWater = 100f;
    public float water = 85f;
    public int intoxication = 0;
    public int search = 0;

    public Inventory inventory;
    public Image foodBar;
    public Image waterBar;
    public Image healthBar;
    [SerializeField] private InventoryUI inventoryUI;
    public Canvas inventoryUIDisplay;
    public PostProcessVolume drunkFX;
    public bool isDrunk = false;
    public bool isOpenInventory = false;

    public Text moneyText;
    public Text searchText;

    private void Awake()
    { 
        
    }

    private void Start()
    {
        inventory = new Inventory(UseItem);
        inventoryUI.SetInventory(inventory);
        inventoryUIDisplay.enabled = false;
        //ItemWorld.SpawnItemWorld(new Vector3(10, 10), new Item { itemType = Item.ItemType.Drugs, amount = 1 });
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Beer:
                intoxication += 5;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Beer, amount = 1 });
                break;
            case Item.ItemType.Drugs:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Drugs, amount = 1 });
                intoxication += 15;
                break;
            case Item.ItemType.Food:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Food, amount = 1 });
                food += 10;
                break;
            case Item.ItemType.Gun:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Gun, amount = 1 });
                break;
            case Item.ItemType.Water:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Water, amount = 1 });
                water += 10;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryUIDisplay.enabled == true)
            {
                //Close Inventory
                inventoryUIDisplay.enabled = false;
                isOpenInventory = false;

            } else
            {
                //Open Inventory
                inventoryUIDisplay.enabled = true;
                isOpenInventory = true;
            }
        }

    }

    private void CheckStatus()
    {
        healthBar.fillAmount = health / maxHealth;
        foodBar.fillAmount = food / maxFood;
        waterBar.fillAmount = water / maxWater;

        if (intoxication > 0)
        {
            isDrunk = true;
            drunkFX.enabled = true;
        }
        else
        {
            isDrunk = false;
            drunkFX.enabled = false;
        }

        moneyText.text = "$" + money;
        searchText.text = "Search level: " + search.ToString();
    }
}
