using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : ShootingEnemy
{
    public Transform barrelEnd;
    private Weapon pistol;

    protected override void InitializeEnemy()
    {
        speed = 1.5f;
        shootingRange = 10f;
        shootingCooldown = 1.5f;

        // Initialize the pistol weapon
        pistol = new Weapon(WeaponType.Pistol, barrelEnd);
    }

    protected override void ShootAtPlayer()
    {
        // Use the pistol weapon to shoot
        pistol.Shoot();
    }

}
