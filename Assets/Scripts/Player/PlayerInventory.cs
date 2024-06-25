using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0;
    public Transform barrelEnd;
    public SpriteRenderer playerSpriteRenderer;

    public Sprite spritePistol;
    public Sprite spriteShotgun;
    public Sprite spriteRifle;

    public GameObject pistolUI;
    public GameObject shotgunUI;
    public GameObject rifleUI;

    public TMP_Text ammoText;
    public TMP_Text magazineText;

    private void Start()
    {
        AddWeapon(WeaponType.Pistol);

        UpdateWeaponVisuals();
        UpdateAmmoUI();
    }

    private bool HasWeapon(WeaponType weaponType, out Weapon weapon)
    {
        foreach (Weapon w in weapons)
        {
            if (w.weaponType == weaponType)
            {
                weapon = w;
                return true;
            }
        }
        weapon = null;
        return false;
    }

    public void AddWeapon(WeaponType weaponType)
    {
        if (!HasWeapon(weaponType, out Weapon existingWeapon))
        {
            Weapon newWeapon = new Weapon(weaponType, barrelEnd);
            weapons.Add(newWeapon);
            Debug.Log(newWeapon.name + " added to inventory.");
            UpdateWeaponVisuals();
            UpdateAmmoUI();
        }
        else
        {
            existingWeapon.PickUpMagazine(2);
            UpdateAmmoUI();
            Debug.Log("Added magazine to existing " + weaponType);
        }
    }

    public void UseCurrentWeapon()
    {
        if (weapons.Count > 0 && weapons[currentWeaponIndex].ammo > 0)
        {
            weapons[currentWeaponIndex].Shoot();
            UpdateAmmoUI();
        }
        else
        {
            Debug.Log(weapons[currentWeaponIndex].name + " has no ammo left.");
        }
    }

    public void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        Debug.Log("Switched to " + weapons[currentWeaponIndex].name);
        UpdateWeaponVisuals();
        UpdateAmmoUI();
    }

    private void UpdateWeaponVisuals()
    {
        pistolUI.SetActive(false);
        shotgunUI.SetActive(false);
        rifleUI.SetActive(false);

        switch (weapons[currentWeaponIndex].weaponType)
        {
            case WeaponType.Pistol:
                playerSpriteRenderer.sprite = spritePistol;
                pistolUI.SetActive(true);
                break;
            case WeaponType.Shotgun:
                playerSpriteRenderer.sprite = spriteShotgun;
                shotgunUI.SetActive(true);
                break;
            case WeaponType.Rifle:
                playerSpriteRenderer.sprite = spriteRifle;
                rifleUI.SetActive(true);
                break;
        }
    }

    private void UpdateAmmoUI()
    {
        if (weapons.Count > 0)
        {
            Weapon currentWeapon = weapons[currentWeaponIndex];
            ammoText.text = "Ammo: " + currentWeapon.ammo + " / " + currentWeapon.features.maxAmmo;
            magazineText.text = "Magazines: " + currentWeapon.features.magazineCount;
        }
    }

    public void ReloadCurrentWeapon()
    {
        if (weapons.Count > 0)
        {
            weapons[currentWeaponIndex].Reload();
            UpdateAmmoUI();
        }
    }

    public void PickUpMagazine(WeaponType weaponType, int magazineCount)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.weaponType == weaponType)
            {
                weapon.PickUpMagazine(magazineCount);
                UpdateAmmoUI();
                return;
            }
        }
    }
}
