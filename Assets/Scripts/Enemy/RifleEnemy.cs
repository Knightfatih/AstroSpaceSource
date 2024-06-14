using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleEnemy : ShootingEnemy
{
    public Transform barrelEnd;

    protected override void InitializeEnemy()
    {
        speed = 1f;
        shootingRange = 20f;
        shootingCooldown = 2f;
    }

}
