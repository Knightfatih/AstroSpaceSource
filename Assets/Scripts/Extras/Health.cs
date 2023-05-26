using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Health : CollectableItem
    {
        [SerializeField] int healAmount = 50;

    protected override void GiveItem()
    {
        playerInventory.AddHealth(healAmount);
    }
}