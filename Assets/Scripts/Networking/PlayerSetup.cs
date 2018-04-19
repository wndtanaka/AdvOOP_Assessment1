using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerController))]
    public class PlayerSetup : NetworkBehaviour
    {
        [SerializeField]
        Behaviour[] componentToDisable;
        [SerializeField]
        GameObject playerGraphics;
        [SerializeField]
        GameObject playerUIPrefab;

        [HideInInspector]
        public GameObject playerUIInstance;

        string remotePlayerName = "RemotePlayer";
        string doNotDisplayLayerName = "Do not display";

        void Start()
        {
            if (!isLocalPlayer)
            {
                DisableComponents();
                AssignRemotePlayer();
            }
            else
            {
                // Create PlayerUI
                playerUIInstance = Instantiate(playerUIPrefab);
                playerUIInstance.name = playerUIPrefab.name;

                // disable playerGraphics for local player
                SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(doNotDisplayLayerName));

                // Configure PlayerUI
                PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
                if (ui == null)
                {
                    Debug.LogError("No PlayerUI component on PlayerUI prefab");
                }
                ui.SetPlayer(GetComponent<Player>());

                GetComponent<Player>().SetupPlayer();
            }
        }

        private void SetLayerRecursively(GameObject obj, int newLayer)
        {
            obj.layer = newLayer;

            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            string _netID = GetComponent<NetworkIdentity>().netId.ToString();
            Player _player = GetComponent<Player>();

            GameManager.RegisterPlayer(_netID, _player);
        }

        void AssignRemotePlayer()
        {
            gameObject.layer = LayerMask.NameToLayer(remotePlayerName);
        }

        void DisableComponents()
        {
            for (int i = 0; i < componentToDisable.Length; i++)
            {
                componentToDisable[i].enabled = false;
            }
        }

        private void OnDisable()
        {
            Destroy(playerUIInstance);

            if (isLocalPlayer)
            {
                GameManager.instance.SetSceneCameraActive(true);
            }

            GameManager.UnRegisterPlayer(transform.name);
        }
    }
}