using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMaoWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ThirdPersonController player;
            PlayerHandler handler;

            other.gameObject.TryGetComponent<ThirdPersonController>(out player);
            other.gameObject.TryGetComponent<PlayerHandler>(out handler);

            player.falling.y = 10;
            handler.health -= 20;
        }
    }
}
