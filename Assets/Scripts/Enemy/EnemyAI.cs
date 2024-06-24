using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(HealthManager))]
public abstract class EnemyAI : MonoBehaviour
{
    public float speed;
    public float patrollingSpeed;
    public float chasingSpeed;
    public float shootingRange;
    public Transform player;
    public LayerMask obstacleMask;
    public int damageAmount = 10;

    // Patrol settings
    public List<Transform> patrolPoints;
    protected int currentPatrolIndex = 0;
    protected bool isPatrolling = true;
    protected bool playerInSight = false;
    protected Transform lastPatrolPoint;

    private Rigidbody2D rb2D;
    private HealthManager enemyHealth;

    protected virtual void Start()
    {
        InitializeEnemy();

        rb2D = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<HealthManager>();

        if (rb2D != null)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

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
            StopPatrolling();
            speed = chasingSpeed;

            if (this is UnarmedEnemy)
            {
                MoveTowardsPlayer();
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
                speed = patrollingSpeed;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    protected void RotateTowardsPatrolPoint(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
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
            }
            else
            {
                Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.red);
            }
        }
        return false;
    }

    protected void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb2D.velocity = direction * speed;
        }
    }

    protected virtual void ShootAtPlayer()
    {
        // Empty base method to be overridden in derived classes
    }

    protected void Patrol()
    {
        if (patrolPoints.Count == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        lastPatrolPoint = targetPatrolPoint;
        RotateTowardsPatrolPoint(targetPatrolPoint.position);
        Vector2 direction = (targetPatrolPoint.position - transform.position).normalized;
        rb2D.velocity = direction * speed;

        if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
    }

    protected void MoveTowardsLastPatrolPoint()
    {
        if (lastPatrolPoint == null) return;

        RotateTowardsPatrolPoint(lastPatrolPoint.position);
        Vector2 direction = (lastPatrolPoint.position - transform.position).normalized;
        rb2D.velocity = direction * speed;

        if (Vector3.Distance(transform.position, lastPatrolPoint.position) < 0.1f)
        {
            isPatrolling = true;
            lastPatrolPoint = null;
        }
    }

    protected void StopPatrolling()
    {
        rb2D.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (rb2D.velocity.magnitude < 0.1f)
        {
            rb2D.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb2D.velocity = Vector2.zero;
            StartDealingDamage(other.GetComponent<HealthManager>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopDealingDamage();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb2D.velocity = Vector2.zero;
        }
    }

    private Coroutine damageCoroutine;

    private void StartDealingDamage(HealthManager playerHealth)
    {
        if (damageCoroutine == null && playerHealth != null)
        {
            damageCoroutine = StartCoroutine(DealDamageOverTime(playerHealth));
        }
    }

    private void StopDealingDamage()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DealDamageOverTime(HealthManager playerHealth)
    {
        while (true)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
