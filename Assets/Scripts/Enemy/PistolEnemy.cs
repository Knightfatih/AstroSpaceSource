using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : ShootingEnemy
{
    protected override void InitializeEnemy()
    {
        speed = 1.5f;
        shootingRange = 10f;
        shootingCooldown = 1f;
    }

}
