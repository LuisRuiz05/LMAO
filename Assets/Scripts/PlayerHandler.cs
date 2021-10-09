using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public int money = 0;
    float maxHealth = 100;
    public float health = 100;
    float maxFood = 100f;
    public float food = 100;
    float maxWater = 100f;
    public float water = 100f;
    public float intoxication = 0;
    public float search = 0;

    bool lowingFood = false;
    bool lowingWater = false;
    bool detoxicating = false;
    bool hasAlly = false;

    public Inventory inventory;
    public Image foodBar;
    public Image waterBar;
    public Image healthBar;
    [SerializeField] private InventoryUI inventoryUI;
    public ThirdPersonController thirdPersonController;
    public Canvas inventoryUIDisplay;
    public PostProcessVolume drunkFX;
    public bool isDrunk = false;
    public bool isOpenInventory = false;
    public GameObject allyPrefab;

    public Text moneyText;
    public Text searchText;

    private void Start()
    {
        inventory = new Inventory(UseItem);
        inventoryUI.SetInventory(inventory);
        inventoryUIDisplay.enabled = false;
        //ItemWorld.SpawnItemWorld(new Vector3(10, 10), new Item { itemType = Item.ItemType.Drugs, amount = 1 });
    }

    // Update is called once per frame
    void Update()
    {
        CheckAlive();
        CheckStatus();
        UpdateStatus();

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
    void UpdateStatus()
    {
        if(food > 100)
        {
            food = 100;
        } 
        if (water > 100)
        {
            water = 100;
        }
        if (intoxication < 0)
        {
            intoxication = 0;
        }
        if (search < 0)
        {
            search = 0;
        }

        if (food > 0 && !lowingFood)
        {
            lowingFood = true;
            StartCoroutine(GetFoodDown());
        }
        if (water > 0 && !lowingWater)
        {
            lowingWater = true;
            StartCoroutine(GetWaterDown());
        }
        if (intoxication > 0 && !detoxicating)
        {
            detoxicating = true;
            StartCoroutine(GetIntoxicationDown());
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
            SpawnAlly();
        }
        else
        {
            isDrunk = false;
            drunkFX.enabled = false;
            hasAlly = false;
        }

        moneyText.text = "$" + money;
        searchText.text = "Search level: " + search.ToString();
    }

    void SpawnAlly()
    {
        if (!hasAlly)
        {
            GameObject ally = Instantiate(allyPrefab);
            hasAlly = true;
        }
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
                intoxication -= 1f;
                break;
            case Item.ItemType.Gun:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Gun, amount = 1 });
                break;
            case Item.ItemType.Water:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Water, amount = 1 });
                water += 10;
                intoxication -= 2f;
                break;
            case Item.ItemType.Medkit:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Medkit, amount = 1 });
                health += 10;
                break;
        }
    }

    void CheckAlive(){
        if (health <= 0)
        {
            thirdPersonController.Die();
            StartCoroutine(LoadDeathScreen());
        }
    }

    IEnumerator GetWaterDown()
    {
        yield return new WaitForSecondsRealtime(30);
        water -= 1;
        lowingWater = false;
    }
    IEnumerator GetFoodDown()
    {
        yield return new WaitForSecondsRealtime(40);
        food -= 1;
        lowingFood = false;
    }
    IEnumerator GetIntoxicationDown()
    {
        yield return new WaitForSecondsRealtime(50);
        intoxication -= 1;
        detoxicating = false;
    }

    IEnumerator LoadDeathScreen()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(3);
    }
}
