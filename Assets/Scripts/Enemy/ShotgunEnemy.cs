using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : ShootingEnemy
{
    public float spreadAngle = 15f;
    public Transform barrelEnd;

    protected override void InitializeEnemy()
    {
        speed = 1.2f;
        shootingRange = 10f;
        shootingCooldown = 3f;
    }

    protected override void ShootAtPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate the spread directions
        Vector2 direction1 = Quaternion.Euler(0, 0, -spreadAngle) * directionToPlayer;
        Vector2 direction2 = directionToPlayer;
        Vector2 direction3 = Quaternion.Euler(0, 0, spreadAngle) * directionToPlayer;

        // Shoot three bullets
        ShootBullet(direction1);
        ShootBullet(direction2);
        ShootBullet(direction3);
    }

    private void ShootBullet(Vector2 direction)
    {
        Vector2 rayOrigin = barrelEnd.position;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, shootingRange, obstacleMask);
        Debug.DrawRay(rayOrigin, direction * shootingRange, Color.yellow, 1f);

        if (hit.collider != null)
        {
            if (hit.transform == player)
            {
                Debug.Log(gameObject.name + " hit the player with a shotgun bullet at " + Time.time);
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }
            }
            else
            {
                Debug.Log(gameObject.name + " shotgun bullet blocked by " + hit.collider.name);
            }
        }
    }
}
