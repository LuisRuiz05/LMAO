using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite foodSprite;
    public Sprite waterSprite;
    public Sprite beerSprite;
    public Sprite drugsSprite;
    public Sprite gunSprite;
}
