using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : ShootingEnemy
{
    public Transform barrelEnd;
    private Weapon shotgun;

    protected override void InitializeEnemy()
    {
        speed = 1.2f;
        shootingRange = 10f;
        shootingCooldown = 3f;

        shotgun = new Weapon(WeaponType.Shotgun, barrelEnd);
    }

    protected override void ShootAtPlayer()
    {
        shotgun.Shoot();
    }
}
