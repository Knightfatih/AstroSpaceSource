using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public WeaponType weaponType;
    public string name;
    public Transform barrelEnd;
    public int ammo;
    public WeaponFeatures features;
    public float spreadAngle = 15f;

    public LayerMask wallLayerMask;

    public Weapon(WeaponType weaponType, Transform barrelEnd)
    {
        this.weaponType = weaponType;
        this.barrelEnd = barrelEnd;
        InitializeWeapon();
        wallLayerMask = LayerMask.GetMask("Wall");
    }

    private void InitializeWeapon()
    {
        switch (weaponType)
        {
            case WeaponType.Pistol:
                name = "Pistol";
                features = new WeaponFeatures
                {
                    damage = 10,
                    range = 10f,
                    maxAmmo = 15,
                    magazineCount = 3,
                    maxMagazines = 5,
                    reloadTime = 1.5f
                };
                break;
            case WeaponType.Shotgun:
                name = "Shotgun";
                features = new WeaponFeatures
                {
                    damage = 20,
                    range = 7f,
                    maxAmmo = 8,
                    magazineCount = 2,
                    maxMagazines = 3,
                    reloadTime = 2.0f
                };
                break;
            case WeaponType.Rifle:
                name = "Rifle";
                features = new WeaponFeatures
                {
                    damage = 15,
                    range = 15f,
                    maxAmmo = 30,
                    magazineCount = 4,
                    maxMagazines = 5,
                    reloadTime = 2.5f
                };
                break;
        }
        ammo = features.maxAmmo;
    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;

            if (weaponType == WeaponType.Shotgun)
            {
                ShootShotgun();
            }
            else
            {
                ShootBullet(barrelEnd.up);
            }

            if (ammo == 0)
            {
                AutoReload();
            }
        }
        else
        {
            Debug.Log(name + " is out of ammo!");
        }
    }

    private void ShootBullet(Vector2 direction)
    {
        Vector2 rayOrigin = barrelEnd.position;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, features.range, wallLayerMask | LayerMask.GetMask("Default", "Player", "Enemy"));
        Debug.DrawRay(rayOrigin, direction * features.range, Color.red, 1f);

        if (hit.collider != null)
        {
            HealthManager targetHealth = hit.collider.GetComponent<HealthManager>();
            WallHealth wallHealth = hit.collider.GetComponent<WallHealth>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(features.damage);
            }
            else if (wallHealth != null)
            {
                wallHealth.TakeDamage(features.damage);
            }
        }
    }

    private void ShootShotgun()
    {
        Vector2 directionToPlayer = barrelEnd.up;

        // Calculate the spread directions
        Vector2 direction1 = Quaternion.Euler(0, 0, -spreadAngle) * directionToPlayer;
        Vector2 direction2 = directionToPlayer;
        Vector2 direction3 = Quaternion.Euler(0, 0, spreadAngle) * directionToPlayer;

        // Shoot three bullets
        ShootBullet(direction1);
        ShootBullet(direction2);
        ShootBullet(direction3);
    }

    public void Reload()
    {
        if (features.magazineCount > 0)
        {
            features.magazineCount--;
            ammo = features.maxAmmo;
            Debug.Log(name + " reloaded. Magazines left: " + features.magazineCount);
        }
        else
        {
            Debug.Log(name + " is out of magazines!");
        }
    }

    private void AutoReload()
    {
        if (features.magazineCount > 0)
        {
            Reload();
        }
    }

    public void PickUpMagazine(int count)
    {
        features.magazineCount = Mathf.Min(features.magazineCount + count, features.maxMagazines);
        Debug.Log("Picked up " + count + " magazine(s). Magazines now: " + features.magazineCount);
    }
}
