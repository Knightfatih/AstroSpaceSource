using UnityEngine;

public class UnarmedEnemy : EnemyAI
{
    public int damageAmount = 10;
    public float damageInterval = 1f;
    private bool isTouchingPlayer = false;
    private float nextDamageTime = 0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void InitializeEnemy()
    {
        speed = 3f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isTouchingPlayer && Time.time >= nextDamageTime)
        {
            Debug.Log("Player got punched");
            PlayerHealth playerComponent = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerComponent != null)
            {
                playerComponent.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
