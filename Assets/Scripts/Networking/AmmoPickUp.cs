using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class AmmoPickUp : PickUpItem
    {
        [SerializeField]
        int ammoReplenish = 10;

        public override void PickUp()
        {
            weaponManager.currentWeapon.bullets += ammoReplenish;
            Destroy(gameObject);
        }
    }
}