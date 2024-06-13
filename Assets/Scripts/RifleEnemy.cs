using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleEnemy : EnemyAI
{
    protected override void InitializeEnemy()
    {
        speed = 1f;
        shootingRange = 20f;
    }

    protected override void ShootAtPlayer()
    {
        // Rifle shooting logic
        //Debug.Log("Shooting at player with rifle");
    }

}
