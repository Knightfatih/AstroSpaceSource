using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI1 : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 3f;
    public float lineOfSite;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = player.position - transform.position;
        //Debug.Log(direction);
        float angle =Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        direction.Normalize();
        movement = direction;

        

    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        //print("Player Location" + player.position);
        if (distanceFromPlayer < lineOfSite)
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
            
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);

    }
}
