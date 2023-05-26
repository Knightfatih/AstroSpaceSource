using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int BosshealthMax;
    int health = 200;
    public HealthBar healthBar;

    public YouWon youwonUI;

    public AudioManager AM;

    private void Start()
    {
        AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        youwonUI = GameObject.FindGameObjectWithTag("Boss").GetComponent<YouWon>();
        healthBar = GameObject.Find("EnemyHealthbar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(BosshealthMax);
    }

    public void DealDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            AM.AudioBossDestroy();
            YouWonCall();
            
            Destroy(gameObject);
            
        }
        healthBar.SetHealth(health);
    }

    public void YouWonCall()
    {
        youwonUI.bossDeath = true; 
    }

}
