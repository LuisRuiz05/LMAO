using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Vector3 playerPosition;
    private PlayerHandler player;

    int playerKeyInput = 0;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHandler>();
        playerPosition = GameObject.Find("Player").transform.position;
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    private void Update()
    {
        if (player.isOpenInventory)
        {
            int invLen = inventory.GetItemList().Count;
            if (Input.GetKeyDown(KeyCode.Alpha1)){
                playerKeyInput = 0;
                if (playerKeyInput <= invLen - 1)
                {
                    inventory.UseItem(inventory.GetItemList()[playerKeyInput]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                playerKeyInput = 1;
                if (playerKeyInput <= invLen - 1)
                {
                    inventory.UseItem(inventory.GetItemList()[playerKeyInput]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                playerKeyInput = 2;
                if (playerKeyInput <= invLen - 1)
                {
                    inventory.UseItem(inventory.GetItemList()[playerKeyInput]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                playerKeyInput = 3;
                if (playerKeyInput <= invLen - 1)
                {
                    inventory.UseItem(inventory.GetItemList()[playerKeyInput]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                playerKeyInput = 4;
                if (playerKeyInput <= invLen - 1)
                {
                    inventory.UseItem(inventory.GetItemList()[playerKeyInput]);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                playerKeyInput = 5;
                if (playerKeyInput <= invLen - 1)
                {
                    inventory.UseItem(inventory.GetItemList()[playerKeyInput]);
                }
            }
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged (object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
        

        float x = -2;
        float y = 0.15f;
        float itemSlotCellSize = 80f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            Text txt = itemSlotRectTransform.Find("amountText").GetComponent<Text>();
            txt.text = item.amount.ToString();
            
            x++;
            if (x > 2)
            {
                x = -2;
                y--;
            }
        }
    }
}
