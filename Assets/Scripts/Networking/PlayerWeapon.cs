using UnityEngine;

namespace Networking
{
    [System.Serializable]
    public class PlayerWeapon
    {
        public string name = "Pistol";
        public int damage = 10;
        public float range = 100f;

        public float fireRate = 7;

        public int maxBullets = 20;
        //[HideInInspector]
        public int bullets;

        public float reloadTime = 2f;

        public GameObject graphics;

        public PlayerWeapon()
        {
            bullets = maxBullets;
        }
    }
}