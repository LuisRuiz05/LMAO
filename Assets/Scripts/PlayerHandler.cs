using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public int money = 0;
    public int health = 100;
    public int intoxication = 0;
    public int search = 0;

    public Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;
    public GameObject inventoryUIDisplay;
    public PostProcessVolume drunkFX;
    public bool isDrunk = false;

    public Text moneyText;
    public Text searchText;

    private void Awake()
    { 
        
    }

    private void Start()
    {
        inventory = new Inventory();
        inventoryUI.SetInventory(inventory);
        //ItemWorld.SpawnItemWorld(new Vector3(10, 10), new Item { itemType = Item.ItemType.Drugs, amount = 1 });
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrunk)
        {
            drunkFX.enabled = true;
        }
        else
        {
            drunkFX.enabled = false;
        }
        moneyText.text = "$" + money;
        searchText.text = "Search level: " + search.ToString();

        /*if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryUIDisplay.enabled == true)
            {
                inventoryUIDisplay.enabled = false;
            } else
            {
                inventoryUIDisplay. = true;
            }
        }*/

    }
}
