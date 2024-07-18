using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Sprite[] damageSprites;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            boxCollider2D.enabled = false;
            UpdateSprite();
        }
        else
        {
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        int spriteIndex = Mathf.FloorToInt((1 - (float)currentHealth / maxHealth) * (damageSprites.Length - 1));
        spriteRenderer.sprite = damageSprites[spriteIndex];
    }
}
