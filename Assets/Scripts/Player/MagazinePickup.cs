using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinePickup : MonoBehaviour
{
    public WeaponType weaponType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.PickUpMagazine(weaponType, 2);
                Destroy(gameObject);
            }
        }
    }
}
