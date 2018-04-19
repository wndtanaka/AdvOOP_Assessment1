using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class PickUpItem : MonoBehaviour
    {
        protected Player player;
        protected WeaponManager weaponManager;

        private void Update()
        {
        }
        private void OnTriggerEnter(Collider other)
        {
            player = other.GetComponent<Player>();
            weaponManager = other.GetComponent<WeaponManager>();
            if (other.tag != "Player")
            {
                return;
            }
            Debug.Log("Pickup: " + name);
            PickUp();
        }

        public virtual void PickUp()
        {
        }
    }
}