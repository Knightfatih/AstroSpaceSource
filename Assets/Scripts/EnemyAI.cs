using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Unarmed,
    Pistol,
    Rifle
}

public abstract class EnemyAI : MonoBehaviour
{
    public float speed;
    private float rotationSpeed = 5f;
    public float shootingRange;
    public Transform player;
    public LayerMask obstacleMask;

    [SerializeField] private int health = 50;

    // Patrol settings
    public List<Transform> patrolPoints;
    protected int currentPatrolIndex = 0;
    protected bool isPatrolling = true;
    protected bool playerInSight = false;
    protected Transform lastPatrolPoint;

    protected virtual void Start()
    {
        InitializeEnemy();

        if (patrolPoints.Count > 0)
        {
            transform.position = patrolPoints[currentPatrolIndex].position;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    protected abstract void InitializeEnemy();

    protected virtual void Update()
    {
        playerInSight = RaycastToPlayer();

        if (playerInSight)
        {
            isPatrolling = false;
            RotateTowardsPlayer();

            if (this is UnarmedEnemy)
            {
                MoveTowardsPlayer();
            }
            else
            {
                ShootAtPlayer();
            }
        }
        else
        {
            if (!isPatrolling)
            {
                MoveTowardsLastPatrolPoint();
            }
            else
            {
                Patrol();
            }
        }
    }

    protected void RotateTowardsPlayer()
    {
        if (player == null)
            return;

        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    protected void RotateTowardsPatrolPoint(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    protected bool RaycastToPlayer()
    {
        if (player == null)
            return false;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask);

        if (hit.collider != null)
        {
            if (hit.transform == player)
            {
                Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.green);
                return true;
            }else
                Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.red);
        }
        return false;
    }

    protected void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    protected virtual void ShootAtPlayer()
    {
        // Implement shooting logic in derived classes
    }

    protected void Patrol()
    {
        if (patrolPoints.Count == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        lastPatrolPoint = targetPatrolPoint;
        RotateTowardsPatrolPoint(targetPatrolPoint.position);
        transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
    }

    protected void MoveTowardsLastPatrolPoint()
    {
        if (lastPatrolPoint == null) return;

        RotateTowardsPatrolPoint(lastPatrolPoint.position);
        transform.position = Vector3.MoveTowards(transform.position, lastPatrolPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, lastPatrolPoint.position) < 0.1f)
        {
            isPatrolling = true;
            lastPatrolPoint = null;
        }
    }

    public void DealDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
