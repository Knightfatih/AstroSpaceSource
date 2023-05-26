using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Oxygen : CollectableItem
    {
        [SerializeField] int OxygenAmount = 50;

        protected override void GiveItem()
        {
            playerInventory.AddOxygen(OxygenAmount);
        }
    }