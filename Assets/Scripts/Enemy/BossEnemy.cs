using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : ShootingEnemy
{
    public Transform barrelEnd;
    private Weapon weapon;
    private bool isDodging = false;
    private float dodgeCooldown = 2f;
    private float dodgeCooldownTimer = 0f;

    public Sprite spriteRifle;
    public Sprite spriteShotgun;
    public Sprite spritePistol;
    private SpriteRenderer spriteRenderer;

    protected override void InitializeEnemy()
    {
        speed = 1f;
        patrollingSpeed = 0.5f;
        chasingSpeed = 1.5f;
        shootingRange = 15f;
        shootingCooldown = 2f;

        weapon = new Weapon(WeaponType.Rifle, barrelEnd);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteRifle;
    }

    protected override void Update()
    {
        base.Update();

        if (!isDodging)
        {
            HandleWeaponSwitching();
        }

        HandleDodgeCooldown();
    }

    protected override void ShootAtPlayer()
    {
        weapon.Shoot();
    }

    private void HandleWeaponSwitching()
    {
        if (enemyHealth.currentHealth < enemyHealth.maxHealth * 0.5f && weapon.weaponType != WeaponType.Shotgun)
        {
            weapon = new Weapon(WeaponType.Shotgun, barrelEnd);
            shootingCooldown = 3f;
            spriteRenderer.sprite = spriteShotgun;
        }
        if (enemyHealth.currentHealth < enemyHealth.maxHealth * 0.25f && weapon.weaponType != WeaponType.Pistol)
        {
            weapon = new Weapon(WeaponType.Pistol, barrelEnd);
            shootingCooldown = 1.5f;
            spriteRenderer.sprite = spritePistol;
            StartDodging();
        }
    }

    private void StartDodging()
    {
        if (!isDodging)
        {
            StartCoroutine(Dodge());
        }
    }

    private IEnumerator Dodge()
    {
        isDodging = true;
        Vector2 dodgeDirection = Random.insideUnitCircle.normalized * speed * 2;
        rb2D.velocity = dodgeDirection;
        yield return new WaitForSeconds(0.5f);
        rb2D.velocity = Vector2.zero;
        isDodging = false;
        dodgeCooldownTimer = dodgeCooldown;
    }

    private void HandleDodgeCooldown()
    {
        if (dodgeCooldownTimer > 0)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }
    }
}
