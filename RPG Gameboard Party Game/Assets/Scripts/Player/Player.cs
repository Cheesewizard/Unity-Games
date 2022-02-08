using System;
using Manager.Player;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class Player : NetworkBehaviour
    {
        private Material _playerMaterialClone;
        private NetworkIdentity _networkIdentity;

        [SyncVar(hook = nameof(OnColorChanged))]
        public Color playerColor;

        void OnColorChanged(Color _Old, Color _New)
        {
            _playerMaterialClone = new Material(GetComponentInChildren<Renderer>().material);
            _playerMaterialClone.color = _New;
            GetComponentInChildren<Renderer>().material = _playerMaterialClone;
        }

        [Command]
        private void CmdSetupPlayer(Color _col)
        {
            // player info sent to server, then server updates sync vars which handles it on all clients
            playerColor = _col;
        }


        //Events
        public Action<bool> PlayerMoving;
        public Action<int> PlayerSteps;

        public override void OnStartClient() //OnStartLocalPlayer() 
        {
            GetPlayerId();
            PlayerDataManager.Instance.CmdAddPlayer(_networkIdentity, gameObject);

            var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            CmdSetupPlayer(color);
        }

        private void GetPlayerId()
        {
            var networkIdentity = gameObject.GetComponent<NetworkIdentity>();
            if (networkIdentity != null)
            {
                _networkIdentity = networkIdentity;
            }
        }


        public override void OnStopClient()
        {
            PlayerDataManager.Instance.CmdRemovePlayer(_networkIdentity.netId);
        }

        // Set up player input here. Custom buttons, controller, keyboard etc

        private void OnEnable()
        {
            //PlayerMoving += GameManager.Instance.StartDice;
            //PlayerSteps += UIManager.Instance.UpdateMovement;
        }

        private void OnDisable()
        {
            //PlayerMoving -= GameManager.Instance.StartDice;
            //PlayerSteps -= UIManager.Instance.UpdateMovement;
        }
    }
}