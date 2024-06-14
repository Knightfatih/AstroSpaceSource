using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    public int maxOxygen = 100;
    public float currentOxygen;
    public float breathRate;
    public OxygenBar oxygenBar;

    private void Awake()
    {
        breathRate = 1f;
        currentOxygen = maxOxygen;
        UpdateOxygenBar();
        oxygenBar.SetMaxOxygen(maxOxygen);
        StartCoroutine(ConsumeOxygenContinuously());
    }

    public void ConsumeOxygen(float amount)
    {
        currentOxygen -= amount;
        if (currentOxygen < 0)
        {
            currentOxygen = 0;
            GetComponent<Health>().TakeDamage(GetComponent<Health>().maxHealth);
        }

        UpdateOxygenBar();
    }

    public void ReplenishOxygen(int amount)
    {
        currentOxygen += amount;
        if (currentOxygen > maxOxygen)
        {
            currentOxygen = maxOxygen;
        }

        UpdateOxygenBar();
    }

    private void UpdateOxygenBar()
    {
        if (oxygenBar != null)
        {
            oxygenBar.SetOxygen((int)currentOxygen);
        }
    }

    private IEnumerator ConsumeOxygenContinuously()
    {
        while (true)
        {
            ConsumeOxygen(breathRate);
            yield return new WaitForSeconds(1f);
        }
    }
}
