using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(Oxygen))]
public class PlayerManager : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float dodgeSpeed = 10f;
    public float dodgeDuration = 0.2f;
    public float dodgeCooldown = 1f;

    [Header("Oxygen Settings")]
    public float oxygenConsumptionRate = 5f;
    public int oxygenConsumptionOnDodge = 25;

    private Rigidbody2D rb2d;
    private Vector2 movement;
    private bool isDodging = false;
    private float dodgeTimer = 0f;
    private float dodgeCooldownTimer = 0f;

    private PlayerInventory playerInventory;
    private Oxygen oxygen;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent<PlayerInventory>();
        oxygen = GetComponent<Oxygen>();
    }

    private void Start()
    {
        playerInventory.playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ProcessInputs();
        Aim();

        if (Input.GetMouseButtonDown(0))
        {
            playerInventory.UseCurrentWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerInventory.SwitchWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerInventory.ReloadCurrentWeapon();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (oxygen.currentOxygen > oxygenConsumptionRate)
            {
                oxygen.ConsumeOxygen(oxygenConsumptionRate * Time.deltaTime);
                movement *= runSpeed / moveSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDodging)
        {
            Move();
        }
        HandleDodgeCooldown();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        movement = new Vector2(moveX, moveY);


        if (Input.GetKeyDown(KeyCode.Space) && dodgeCooldownTimer <= 0f)
        {
            StartCoroutine(Dodge());
        }
    }

    private void Move()
    {
        rb2d.velocity = movement * moveSpeed;
    }

    private void Aim()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private IEnumerator Dodge()
    {
        if (oxygen.currentOxygen > oxygenConsumptionOnDodge)
        {
            oxygen.ConsumeOxygen(oxygenConsumptionOnDodge);

            isDodging = true;
            dodgeTimer = dodgeDuration;
            Vector2 dodgeDirection = movement.normalized * dodgeSpeed;
            rb2d.velocity = dodgeDirection;

            while (dodgeTimer > 0)
            {
                dodgeTimer -= Time.deltaTime;
                yield return null;
            }

            isDodging = false;
            dodgeCooldownTimer = dodgeCooldown;
        }
    }

    private void HandleDodgeCooldown()
    {
        if (dodgeCooldownTimer > 0)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }
    }
}
