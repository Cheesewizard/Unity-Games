using System;
using Game.States.GameBoard.StateSystem;
using Manager.Camera;
using Manager.Player;
using Mirror;
using Player;
using UnityEngine;

namespace Game.Player
{
    public class Player : NetworkBehaviour
    {
        private NetworkIdentity _networkIdentity;
        private Material _playerMaterialClone;

        [SyncVar(hook = nameof(OnColorChanged))]
        public Color playerColor;

        [SyncVar(hook = nameof(OnPlayerIdChanged))]
        public int playerId;

        private int _thisPlayerId;

        // Callbacks
        void OnColorChanged(Color _Old, Color _New)
        {
            _playerMaterialClone = new Material(GetComponentInChildren<Renderer>().material);
            _playerMaterialClone.color = _New;
            GetComponentInChildren<Renderer>().material = _playerMaterialClone;
        }

        void OnPlayerIdChanged(int _Old, int _New)
        {
            _thisPlayerId = _New;
        }

        //Events
        public Action<bool> PlayerMoving;
        public Action<int> PlayerSteps;

        public override void OnStartLocalPlayer()
        {
            GetPlayerNetworkIdentity();
            var player = CreatePlayerData();
            PlayerDataManager.Instance.CmdAddPlayerToServer(player);

            var gameBoardSystem = FindObjectOfType<GameBoardSystem>();
            gameBoardSystem.playerId = playerId;
            gameBoardSystem.playerCamera = gameObject.GetComponent<PlayerCameraManager>();

            // Set the camera to player one on game start
            if (playerId == (int) PlayerEnum.Player1)
            {
                gameBoardSystem.playerCamera.CmdEnablePlayerCamera();
            }
            else
            {
                gameBoardSystem.playerCamera.CmdDisablePlayerCamera();
            }
            

            gameBoardSystem.StartGame();
        }


        public override void OnStopClient()
        {
            PlayerDataManager.Instance.CmdRemovePlayer(playerId);
        }

        // Set up player input here. Custom buttons, controller, keyboard etc

        private uint GetPlayerNetworkIdentity()
        {
            _networkIdentity = gameObject.GetComponent<NetworkIdentity>();
            if (_networkIdentity == null) throw new Exception("Could not get network identity from player?");

            return _networkIdentity.netId;
        }

        private PlayerData CreatePlayerData()
        {
            return new PlayerData()
            {
                name = "Philip",
                networkInstanceId = GetPlayerNetworkIdentity(),
                boardLocationIndex = 0,
                PlayerSpeed = 20,
                playerId = _thisPlayerId,
                Identity = _networkIdentity
            };
        }


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