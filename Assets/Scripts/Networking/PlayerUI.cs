using UnityEngine;
using UnityEngine.UI;

namespace Networking
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        RectTransform thrusterFuelFill;

        [SerializeField]
        RectTransform healthBarFill;

        [SerializeField]
        Text ammoCounter;

        private Player player;
        private PlayerController controller;
        private WeaponManager weaponManager;

        public void SetPlayer(Player _player)
        {
            player = _player;
            controller = player.GetComponent<PlayerController>();
            weaponManager = player.GetComponent<WeaponManager>();
        }

        private void Update()
        {
            SetFuelAmount(controller.GetThrusterFuelAmount());
            SetHealthAmount(player.GetHealthPctg());
            SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets);
        }

        void SetFuelAmount(float _amount)
        {
            thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
        }

        void SetHealthAmount(float _amount)
        {
            healthBarFill.localScale = new Vector3(1f, _amount, 1f);
        }

        void SetAmmoAmount(int _amount)
        {
            ammoCounter.text = _amount.ToString();
        }
    }
}