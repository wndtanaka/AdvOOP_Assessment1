using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class WeaponManager : NetworkBehaviour
    {
        [SerializeField]
        private PlayerWeapon primaryWeapon;
        [SerializeField]
        private Transform weaponHolder;
        [HideInInspector]
        public PlayerWeapon currentWeapon;
        private WeaponGraphics currentGraphics;

        [HideInInspector]
        public bool isReloading = false;

        private const string WEAPON_LAYERNAME = "Weapon";

        private void Start()
        {
            EquipWeapon(primaryWeapon);
        }

        public PlayerWeapon GetCurrentWeapon()
        {
            return currentWeapon;
        }
        public WeaponGraphics GetCurrentWeaponGraphics()
        {
            return currentGraphics;
        }

        void EquipWeapon(PlayerWeapon _weapon)
        {
            currentWeapon = _weapon;
            GameObject _weaponInstance = Instantiate(currentWeapon.graphics, weaponHolder.position, weaponHolder.rotation);
            _weaponInstance.transform.SetParent(weaponHolder);

            currentGraphics = _weaponInstance.GetComponent<WeaponGraphics>();
            if (currentGraphics == null)
            {
                Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponInstance.name);
            }

            if (isLocalPlayer)
            {
                Utility.SetLayerRecursively(_weaponInstance, LayerMask.NameToLayer(WEAPON_LAYERNAME));
            }
        }

        public void Reload()
        {
            if (isReloading)
            {
                return;
            }
            StartCoroutine(ExecuteReload());
        }

        IEnumerator ExecuteReload()
        {
            isReloading = true;
            CmdOnReload();
            yield return new WaitForSeconds(currentWeapon.reloadTime);
            currentWeapon.bullets = currentWeapon.maxBullets;
            isReloading = false;
        }

        [Command]
        void CmdOnReload()
        {
            RpcOnReload();
        }
        [ClientRpc]
        void RpcOnReload()
        {
            Animator anim = currentGraphics.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Reload");
            }
        }
    }
}