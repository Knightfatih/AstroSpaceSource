using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPickup : MonoBehaviour
{
    public int oxygenAmount = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Oxygen playerOxygen = other.GetComponent<Oxygen>();
            if (playerOxygen != null)
            {
                playerOxygen.ReplenishOxygen(oxygenAmount);
                Destroy(gameObject);
            }
        }
    }
}
