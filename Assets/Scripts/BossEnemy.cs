using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyAI
{
    public GameObject bossUI;
    public float bossHealth = 1000f;

    protected override void InitializeEnemy()
    {
        // Additional boss-specific logic
    }

    public void TakeDamage(float amount)
    {
        bossHealth -= amount;
        if (bossHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        bossUI.SetActive(true);
        Destroy(gameObject);
        //Change it to dead model
    }
}
