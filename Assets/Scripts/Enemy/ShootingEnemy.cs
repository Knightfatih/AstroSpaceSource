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

    protected override void ShootAtPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, shootingRange, obstacleMask);
        Debug.DrawRay(transform.position, directionToPlayer * shootingRange, Color.cyan, 1f);

        if (hit.collider != null)
        {
            if (hit.transform == player)
            {
                //Debug.Log(gameObject.name + " hit the player at " + Time.time);
                HealthManager playerHealth = player.GetComponent<HealthManager>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }
            else
            {
                //Debug.Log(gameObject.name + " shot blocked by " + hit.collider.name);
            }
        }
    }
}
