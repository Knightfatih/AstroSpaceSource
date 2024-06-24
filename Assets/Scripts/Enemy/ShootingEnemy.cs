using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingEnemy : EnemyAI
{
    protected float shootingCooldown;
    private float shootingTimer;

    protected override void Update()
    {
        base.Update();

        if (playerInSight)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > shootingRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                shootingTimer += Time.deltaTime;

                if (shootingTimer >= shootingCooldown)
                {
                    ShootAtPlayer();
                    shootingTimer = 0f;
                }
            }
        }
    }

    protected abstract void ShootAtPlayer();
}
