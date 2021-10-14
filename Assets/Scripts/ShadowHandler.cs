using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowHandler : MonoBehaviour
{
    public SpriteRenderer shadowSprite;

    // Update is called once per frame
    void Update()
    {
        shadowSprite.transform.position = new Vector3(shadowSprite.transform.position.x, 0f, shadowSprite.transform.position.z);
    }
}
