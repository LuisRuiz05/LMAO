using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    // Start is called before the first frame update
    void Start()
    {
        //itemSlotContainer = transform.Find("itemSlotContainer");
        //itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowItem()
    {
        Debug.Log("Click, click xd");
        //Debug.Log("clicked " + itemSlotTemplate);
    }
}
