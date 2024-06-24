using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Shotgun,
    Rifle
}

public class WeaponPickup : MonoBehaviour
{
    public WeaponType weaponType; // Type of weapon to pick up

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddWeapon(weaponType);
                Destroy(gameObject);
            }
        }
    }
}

