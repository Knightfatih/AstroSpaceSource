using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : ShootingEnemy
{
    public Transform barrelEnd;
    private Weapon weapon;

    protected override void InitializeEnemy()
    {
        speed = 1f;
        shootingRange = 15f;
        shootingCooldown = 2f;

        weapon = new Weapon(WeaponType.Rifle, barrelEnd);
    }

    protected override void ShootAtPlayer()
    {
        weapon.Shoot();
    }
}
