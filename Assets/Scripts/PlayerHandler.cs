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
    float maxXP = 200;
    public float xp = 0;
    public float intoxication = 0;
    public float search = 0;
    float maxAnxiety = 100;
    public int anxiety = 0;
    int minAnxiety = 15;
    public int timeForAnxiety = 30;

    public int level = 1;

    bool lowingFood = false;
    bool lowingWater = false;
    bool detoxicating = false;
    bool boostingAnxiety = false;
    bool healing = false;
    bool hasAlly = false;
    bool isAlive = true;
    
    [SerializeField] private InventoryUI inventoryUI;
    public ThirdPersonController thirdPersonController;
    public Canvas inventoryUIDisplay;
    public PostProcessVolume drunkFX;
    LensDistortion drunkDistortion;
    public bool isDrunk = false;
    public bool isOpenInventory = false;
    public bool isTutorial;
    public GameObject allyPrefab;
    public ParticleSystem blood;
    SoundFXManager soundFX;

    public Inventory inventory;
    public Image foodBar;
    public Image waterBar;
    public Image healthBar;
    public Image xpBar;
    public Text moneyText;
    public Text searchText;
    public Text levelText;

    private void Start()
    {
        if (GameObject.Find("Tutorial") != null)
        {
            isTutorial = true;
            money = 25;
        }
        else
        {
            isTutorial = false;
        }

        drunkFX.profile.TryGetSettings(out drunkDistortion);
        inventory = new Inventory(UseItem, isTutorial);
        inventoryUI.SetInventory(inventory);
        inventoryUIDisplay.enabled = false;
        soundFX = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundFXManager>();
        //ItemWorld.SpawnItemWorld(new Vector3(10, 10), new Item { itemType = Item.ItemType.Drugs, amount = 1 });
    }

    // Update is called once per frame
    void Update()
    {
        CheckAlive();
        CheckStatus();
        CheckPositionInMap();
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
        if (food <= 0)
        {
            health -= 1 * Time.deltaTime;
        }
        if (water > 100)
        {
            water = 100;
        }
        if (water <= 0)
        {
            health -= 1 * Time.deltaTime;
        }
        if (intoxication < 0)
        {
            intoxication = 0;
        }
        if (intoxication >= 100)
        {
            health -= 100;
        }
        if (search < 0)
        {
            search = 0;
        } if (search > 550)
        {
            search = 550;
        } if (anxiety < 0)
        {
            anxiety = 0;
        } 
        
        
        if (anxiety >= maxAnxiety)
        {
            health -= 1 * Time.deltaTime;
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
        if (health < 65 && !healing)
        {
            healing = true;
            StartCoroutine(GetHealed());
        }
        if (intoxication > 0 && !detoxicating)
        {
            detoxicating = true;
            StartCoroutine(GetIntoxicationDown());
        }
        if (!boostingAnxiety)
        {
            boostingAnxiety = true;
            StartCoroutine(BoostAnxiety());
        }
    }

    private void CheckStatus()
    {
        healthBar.fillAmount = health / maxHealth;
        foodBar.fillAmount = food / maxFood;
        waterBar.fillAmount = water / maxWater;
        xpBar.fillAmount = xp / maxXP;

        if (intoxication > 0)
        {
            isDrunk = true;
            drunkFX.enabled = true;
            drunkDistortion.intensity.value = -intoxication;
            SpawnAlly();
        }
        else
        {
            isDrunk = false;
            drunkFX.enabled = false;
            hasAlly = false;
        }

        if (xpBar.fillAmount == 1)
        {
            xp -= maxXP;
            maxXP += 25;
            level++;
        }

        moneyText.text = "$" + money;
        searchText.text = "Search level: " + search.ToString();
        levelText.text = level.ToString();
    }

    void SpawnAlly()
    {
        if (!hasAlly)
        {
            GameObject ally = Instantiate(allyPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z-2), transform.rotation);
            hasAlly = true;
        }
    }

    void CheckPositionInMap()
    {
        Vector3 pos = gameObject.transform.position;
        if(pos.y < -6)
        {
            pos.y = 4;
            gameObject.transform.position = pos;
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Beer:
                soundFX.source.PlayOneShot(soundFX.beer);
                intoxication += 8;
                water -= 1;
                anxiety -= 8;
                if (timeForAnxiety > minAnxiety)
                {
                    timeForAnxiety -= 1;
                }
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Beer, amount = 1 });
                break;
            case Item.ItemType.Drugs:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Drugs, amount = 1 });
                intoxication += 15;
                anxiety -= 25;
                if (timeForAnxiety > minAnxiety)
                {
                    timeForAnxiety -= 2;
                }
                break;
            case Item.ItemType.Food:
                soundFX.source.PlayOneShot(soundFX.food);
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Food, amount = 1 });
                food += 15;
                intoxication -= 2f;
                break;
            case Item.ItemType.Gun:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Gun, amount = 1 });
                break;
            case Item.ItemType.Water:
                soundFX.source.PlayOneShot(soundFX.water);
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Water, amount = 1 });
                water += 15;
                intoxication -= 4f;
                break;
            case Item.ItemType.Medkit:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Medkit, amount = 1 });
                health += 40;
                break;
        }
    }

    void CheckAlive(){
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            soundFX.source.PlayOneShot(soundFX.playersDeath);
            thirdPersonController.Die();
            StartCoroutine(LoadDeathScreen());
        }
    }

    IEnumerator GetWaterDown()
    {
        yield return new WaitForSecondsRealtime(5);
        if (!isDrunk)
        {
            water -= 1;
        } else
        {
            water -= 2;
        }
        lowingWater = false;
    }
    IEnumerator GetFoodDown()
    {
        yield return new WaitForSecondsRealtime(6);
        food -= 1;
        lowingFood = false;
    }
    IEnumerator GetHealed()
    {
        yield return new WaitForSecondsRealtime(2);
        health += 1;
        healing = false;
    }
    IEnumerator GetIntoxicationDown()
    {
        yield return new WaitForSecondsRealtime(15);
        intoxication -= 1;
        detoxicating = false;
    }
    
    IEnumerator BoostAnxiety()
    {
        yield return new WaitForSecondsRealtime(timeForAnxiety);
        anxiety += 10;
        boostingAnxiety = false;
    }

    IEnumerator LoadDeathScreen()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(4);
    }

}
