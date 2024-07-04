using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : ShootingEnemy
{
    public Transform barrelEnd;
    private Weapon weapon;

    public Sprite spriteRifle;
    public Sprite spriteShotgun;
    public Sprite spritePistol;
    private SpriteRenderer spriteRenderer;

    protected override void InitializeEnemy()
    {
        patrollingSpeed = 1.5f;
        chasingSpeed = 1.5f;
        shootingRange = 5f;
        shootingCooldown = 2f;

        weapon = new Weapon(WeaponType.Rifle, barrelEnd);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteRifle;
    }

    protected override void Update()
    {
        base.Update();

        HandleWeaponSwitching();
    }

    protected override void ShootAtPlayer()
    {
        weapon.Shoot();
    }

    private void HandleWeaponSwitching()
    {
        if (enemyHealth.currentHealth < enemyHealth.maxHealth * 0.5f && weapon.weaponType == WeaponType.Rifle)
        {
            weapon = new Weapon(WeaponType.Shotgun, barrelEnd);
            shootingCooldown = 3f;
            spriteRenderer.sprite = spriteShotgun;
            enemyHealth.Heal(enemyHealth.maxHealth);
        }
        else if (enemyHealth.currentHealth < enemyHealth.maxHealth * 0.25f && weapon.weaponType == WeaponType.Shotgun)
        {
            weapon = new Weapon(WeaponType.Pistol, barrelEnd);
            shootingCooldown = 1.5f;
            spriteRenderer.sprite = spritePistol;
            enemyHealth.Heal(enemyHealth.maxHealth);
        }
    }
}
