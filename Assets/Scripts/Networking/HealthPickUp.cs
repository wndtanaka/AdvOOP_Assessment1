using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class HealthPickUp : PickUpItem
    {
        [SerializeField]
        int healthReplenish = 25;

        public override void PickUp()
        {
            if (player.currentHealth == player.maxHealth)
            {
                return;
            }
            if (player.currentHealth + healthReplenish > player.maxHealth)
            {
                player.currentHealth = player.maxHealth;
            }
            else
            {
                player.currentHealth += healthReplenish;
            }

            Destroy(gameObject);
        }
    }
}