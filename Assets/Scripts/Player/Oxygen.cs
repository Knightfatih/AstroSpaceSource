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
    public GameObject lowOxygenWarning;

    private void Awake()
    {
        breathRate = 1f;
        currentOxygen = maxOxygen;
        UpdateOxygenBar();
        oxygenBar.SetMaxOxygen(maxOxygen);
        StartCoroutine(ConsumeOxygenContinuously());
        lowOxygenWarning.SetActive(false);
    }

    public void ConsumeOxygen(float amount)
    {
        currentOxygen -= amount;
        if (currentOxygen < 0)
        {
            currentOxygen = 0;
            GetComponent<HealthManager>().TakeDamage(GetComponent<HealthManager>().maxHealth);
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
        if (currentOxygen < 24)
        {
            if (!lowOxygenWarning.activeSelf)
            {
                StartCoroutine(FlashLowOxygenWarning());
            }
        }
        else
        {
            if (lowOxygenWarning.activeSelf)
            {
                StopCoroutine(FlashLowOxygenWarning());
                lowOxygenWarning.SetActive(false);
            }
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

    private IEnumerator FlashLowOxygenWarning()
    {
        while (currentOxygen < 24)
        {
            lowOxygenWarning.SetActive(!lowOxygenWarning.activeSelf);
            yield return new WaitForSeconds(0.25f);
        }
        lowOxygenWarning.SetActive(false);
    }
}
