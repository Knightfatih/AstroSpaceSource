using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCHealth : MonoBehaviour
{
    [SerializeField] int health = 50;

    public AudioManager AM;

    private void Start()
    {
        AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void DealDamage(int damageAmount)
    {
        health -= damageAmount;
        AM.AudioNPCHit();
        if (health < 0)
        {
            AM.AudioNPCDestroy();
            Destroy(gameObject);
        }
    }
}

