using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : EnemyAI
{
    protected override void InitializeEnemy()
    {
        speed = 1.5f;
        shootingRange = 10f;
    }

    protected override void ShootAtPlayer()
    {
        // Pistol shooting logic
        //Debug.Log("Shooting at player with pistol");
    }

}
