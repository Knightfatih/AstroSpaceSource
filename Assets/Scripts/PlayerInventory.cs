using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    private int currentWeaponIndex = 0;

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = index;
            // Handle weapon switch logic
            Debug.Log("Switched to weapon: " + weapons[currentWeaponIndex].name);
        }
    }

    public Weapon GetCurrentWeapon()
    {
        if (weapons.Count > 0)
        {
            return weapons[currentWeaponIndex];
        }
        return null;
    }

    public void UseCurrentWeapon()
    {
        Weapon currentWeapon = GetCurrentWeapon();
        if (currentWeapon != null)
        {
            currentWeapon.Use();
        }
    }
}
