using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string name;
    public int damage;
    public float range;
    public LayerMask hitLayers;
    public Transform firePoint;

    public void Shoot()
    {
        Vector2 direction = (firePoint.position - firePoint.parent.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range, hitLayers);
        Debug.DrawRay(firePoint.position, direction * range, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            HealthManager targetHealth = hit.collider.GetComponent<HealthManager>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }
    }

    public void Use()
    {
        Shoot();
    }
}
