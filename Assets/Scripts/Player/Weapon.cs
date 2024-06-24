using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public WeaponType weaponType;
    public string name;
    public int damage;
    public float range;
    public LayerMask hitLayers;
    public Transform firePoint;
    public int ammo;
    public int maxAmmo;
    public int magazineCount;
    public int maxMagazines;

    public void Shoot()
    {
        if (ammo > 0)
        {
            ammo--;

            Vector2 direction = (firePoint.position - firePoint.parent.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range, hitLayers);
            Debug.DrawRay(firePoint.position, direction * range, Color.red, 1f);

            if (hit.collider != null)
            {
                HealthManager targetHealth = hit.collider.GetComponent<HealthManager>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(damage);
                }
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

    public void Reload()
    {
        if (magazineCount > 0)
        {
            magazineCount--;
            ammo = maxAmmo;
            Debug.Log(name + " reloaded. Magazines left: " + magazineCount);
        }
        else
        {
            Debug.Log(name + " is out of magazines!");
        }
    }

    private void AutoReload()
    {
        if (magazineCount > 0)
        {
            Reload();
        }
    }

    public void PickUpMagazine()
    {
        if (magazineCount < maxMagazines)
        {
            magazineCount++;
            Debug.Log("Picked up a magazine. Magazines now: " + magazineCount);
        }
    }
}
