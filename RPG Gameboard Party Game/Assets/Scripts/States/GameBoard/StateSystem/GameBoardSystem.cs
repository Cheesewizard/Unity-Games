using System;
using System.Collections;
using Manager.Camera;
using Manager.Dice;
using Manager.Player;
using Manager.Turns;
using Manager.UI;
using Mirror;
using ParrelSync;
using Player;
using States.Menus;
using UnityEngine;

namespace States.GameBoard.StateSystem
{
    public class GameBoardSystem : StateMachine
    {
        public static GameBoardSystem Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }


        // Game Board
        public Route currentRoute;
        [HideInInspector] public GameObject currentTile;

        // Dice
        [HideInInspector] public SyncList<int> diceNumbers = new SyncList<int>();

        // Turn Order
        public TurnManager turnManager;

        // Data
        public PlayerDataManager playerDataManager;
        [SyncVar] [HideInInspector] public PlayerData playerData;
        [HideInInspector] public NetworkIdentity currentAuthority;

        public void Start()
        {
            //DebugQuickConnect();
            playerDataManager = new PlayerDataManager();

            // Start the game flow
            currentState = new CharacterAddScreen(this);
            SetState(currentState);
        }


        private void DebugQuickConnect()
        {
            var manager = FindObjectOfType<NetworkManager>();
            // Debug only
            if (ClonesManager.IsClone())
            {
                // Automatically connect to local host if this is the clone editor
                manager.StartClient();
            }
            else
            {
                // Automatically start server if this is the original editor
                manager.StartHost();
            }
        }

        void Update()
        {
            currentState.Tick();
        }

        public IEnumerator TransitionToState(float timeToWait, GameStates state)
        {
            yield return new WaitForSeconds(timeToWait);
            SetState(state);
        }

        [Server]
        public void ConfirmSetup() // config gamesettings
        {
            UIManager.Instance.SetupPlayerCoinsUI(playerDataManager.CmdGetAllPlayerData());
            turnManager = new TurnManager(playerDataManager.CmdGetTotalPlayers(), 10);
            playerData = playerDataManager.CmdGetPlayerDataFromIndex(turnManager.GetCurrentPlayerIndex());
            currentAuthority = playerData.NetworkIdentity;

            // Set camera to player 1 on start
            CameraManager.Instance.RpcSpawnCamerasIntoScene();
            CameraManager.Instance.RpcChangeCameraTargetPlayer(CameraEnum.PlayerCamera, playerData.Player.transform);
            DiceManager.Instance.RpcSpawnDice(2);
        }

        public bool CheckIfHasAuthority()
        {
            return currentAuthority == playerData.NetworkIdentity;
        }
    }
}