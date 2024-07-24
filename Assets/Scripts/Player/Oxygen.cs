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
    [SerializeField] private float flashInterval = 0.5f;
    private bool isFlashing = false;

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
            if (!isFlashing)
            {
                StartCoroutine(FlashLowOxygenWarning());
            }
        }
        else
        {
            if (isFlashing)
            {
                StopCoroutine(FlashLowOxygenWarning());
                lowOxygenWarning.SetActive(false);
                isFlashing = false;
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
        isFlashing = true;
        while (currentOxygen < 24)
        {
            lowOxygenWarning.SetActive(true);
            yield return new WaitForSeconds(flashInterval);
            lowOxygenWarning.SetActive(false);
            yield return new WaitForSeconds(flashInterval);
        }
        isFlashing = false;
    }
}
