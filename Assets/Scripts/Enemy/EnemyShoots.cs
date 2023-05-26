using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoots : MonoBehaviour
{
    Transform player;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    private Vector2 movement;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate;
    private float nextFireTime;

    public GameObject bullet;
    public GameObject bulletParent;

    public AudioManager AM;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;            
        }
        else if (player!= null)
        {
            Vector3 direction = player.position - transform.position;
            //Debug.Log(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;

            direction.Normalize();
            movement = direction;
        }
    }

    private void FixedUpdate()
    {
        if (movement != null)
        {
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        if (player != null)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        
            if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
            {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
                AM.AudioNPCFireShot();
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
}
