using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public bool playerDeath { get; set; }
    public bool bossDeath { get; set; }

    public static bool GameIsPaused = false;

    public GameObject gameOverUI;
    public GameObject youWonUI;

    private void Awake()
    {
        maxHealth = DifficultyManager.Instance.GetMaxHealth();
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            UpdateHealthBar();
        }
    }

    private void Start()
    {
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (healthBar != null)
        {
            UpdateHealthBar();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (healthBar != null)
        {
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died.");
        //Change the character sprite

        if (playerDeath)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        if (bossDeath)
        {
            youWonUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        Destroy(gameObject, 1f);
    }
}
