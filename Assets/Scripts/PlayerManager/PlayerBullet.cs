using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{

    [SerializeField] int damageAmount;
    [SerializeField] private float speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        NPCHealth npcHealth = collision.gameObject.GetComponent<NPCHealth>();

        if (npcHealth != null)
        {
            npcHealth.DealDamage(damageAmount);
        }
        Destroy(gameObject);

        BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();

        if (bossHealth != null)
        {
            bossHealth.DealDamage(damageAmount);
        }
        Destroy(gameObject);
    }


}
