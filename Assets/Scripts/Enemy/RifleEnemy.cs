using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleEnemy : ShootingEnemy
{
    public Transform barrelEnd;
    private Weapon rifle;

    protected override void InitializeEnemy()
    {
        speed = 1.2f;
        shootingRange = 15f;
        shootingCooldown = 2.5f;

        // Initialize the rifle weapon
        rifle = new Weapon(WeaponType.Rifle, barrelEnd);
    }

    protected override void ShootAtPlayer()
    {
        // Use the rifle weapon to shoot
        rifle.Shoot();
    }

}
