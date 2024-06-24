using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0;
    public Transform firePoint;
    public SpriteRenderer playerSpriteRenderer;

    public Sprite spritePistol;
    public Sprite spriteShotgun;
    public Sprite spriteRifle;

    public GameObject pistolUI;
    public GameObject shotgunUI;
    public GameObject rifleUI;

    public TMP_Text ammoText;
    public TMP_Text magazineText;

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    private void Start()
    {
        weapons.Add(new Weapon
        {
            weaponType = WeaponType.Pistol,
            name = "Pistol",
            damage = 10,
            range = 15f,
            hitLayers = LayerMask.GetMask("Default", "Wall", "Enemy"),
            firePoint = firePoint,
            ammo = 15,
            maxAmmo = 15,
            magazineCount = 3,
            maxMagazines = 5
        });

        UpdateWeaponVisuals();
        UpdateAmmoUI();
    }

    public void AddWeapon(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Shotgun:
                weapons.Add(new Weapon
                {
                    weaponType = WeaponType.Shotgun,
                    name = "Shotgun",
                    damage = 20,
                    range = 7f,
                    hitLayers = LayerMask.GetMask("Default", "Wall", "Enemy"),
                    firePoint = firePoint,
                    ammo = 8,
                    maxAmmo = 8,
                    magazineCount = 2,
                    maxMagazines = 3
                });
                break;
            case WeaponType.Rifle:
                weapons.Add(new Weapon
                {
                    weaponType = WeaponType.Rifle,
                    name = "Rifle",
                    damage = 15,
                    range = 15f,
                    hitLayers = LayerMask.GetMask("Default", "Wall", "Enemy"),
                    firePoint = firePoint,
                    ammo = 30,
                    maxAmmo = 30,
                    magazineCount = 4,
                    maxMagazines = 5
                });
                break;
        }
        Debug.Log(weaponType + " added to inventory.");
        UpdateWeaponVisuals();
        UpdateAmmoUI();
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
            ammoText.text = "Ammo: " + currentWeapon.ammo + " / " + currentWeapon.maxAmmo;
            magazineText.text = "Magazines: " + currentWeapon.magazineCount;
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

    public void PickUpMagazine(WeaponType weaponType)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.weaponType == weaponType)
            {
                weapon.PickUpMagazine();
                UpdateAmmoUI();
                return;
            }
        }
    }
}
