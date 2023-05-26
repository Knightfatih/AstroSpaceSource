using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] int oxygenDamageAmount = 10;

    //public GameObject hitEffect;

    GameObject target;
    public float speed;
    Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRb.velocity = new Vector2(moveDir.x, moveDir.y);
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerInventory playerHealth = collision.gameObject.GetComponent<PlayerInventory>();
        if (playerHealth != null)
        {
            playerHealth.DealDamage(damageAmount);
            playerHealth.DealOxygenDamage(oxygenDamageAmount);

        }
        Destroy(this.gameObject);
    }
}
