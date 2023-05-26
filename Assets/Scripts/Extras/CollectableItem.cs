using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CollectableItem : MonoBehaviour
    {
        protected PlayerInventory playerInventory;

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            playerInventory = collision.gameObject.GetComponent<PlayerInventory>();

            if(playerInventory!= null)
            {
                GiveItem();
                DestroyItem();
            }
        }

        protected virtual void GiveItem()
        {
            //print("Give item!");
        }

        protected void DestroyItem()
        {
            Destroy(gameObject);
        }
    }



