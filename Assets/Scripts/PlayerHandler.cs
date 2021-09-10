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

    public PostProcessVolume drunkFX;
    public bool isDrunk = false;

    public Text moneyText;
    public Text searchText;

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
    }
}
