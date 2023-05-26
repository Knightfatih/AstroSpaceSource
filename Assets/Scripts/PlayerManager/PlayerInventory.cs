using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int oxygenMax;
    public int healthMax;
    [SerializeField] int health = 0;
    [SerializeField] int oxygen = 0;

    public HealthBar healthBar;
    public Timer timerBar;

    public Sprite Death;

    public static bool GameIsPaused = false;
    public GameOver gameoverUI;

    [SerializeField] AudioManager AM;

    private void Start()
    {
        gameoverUI = GameObject.FindGameObjectWithTag("Boss").GetComponent<GameOver>();
        healthBar.SetMaxHealth(healthMax);
        timerBar.SetMaxTime(oxygenMax);
    }


    public void AddHealth(int healAmount)
    {
        AM.AudioCollectableObjects();
        health += healAmount;
        if (health > healthMax)
        {
            health = healthMax;
        }
        healthBar.SetHealth(health);
    }
    public void AddOxygen(int OxygenAmount)
    {
        AM.AudioCollectableObjects();
        oxygen += OxygenAmount;
        if(oxygen> oxygenMax)
        {
            oxygen = oxygenMax;
        }
        timerBar.SetTime(oxygen);
    }
    public void DealOxygenDamage(int oxygenDamageAmount)
    {
        oxygen -= oxygenDamageAmount;

        if (oxygen < 0)
        {
            GameoverCall();

            //GameOverA();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Death;
            Destroy(gameObject, 1f);
            
        }
        timerBar.SetTime(oxygen);
    }

    public void DealDamage(int damageAmount)
    {
        AM.AudioNPCHit();
        health -= damageAmount;

        if (health < 0)
        {
            GameoverCall();

            //GameOver();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Death;
            Destroy(gameObject, 1f);
            
        }

        healthBar.SetHealth(health);
    }

    public void GameoverCall()
    {
        AM.AudioPlayerDestroy();
        gameoverUI.playerDeath = true;
    }
        
}
