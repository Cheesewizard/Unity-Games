using System.Collections;
using System.Collections.Generic;
using Camera;
using Helpers;
using Manager.Camera;
using Manager.Dice;
using Manager.Player;
using Manager.Turns;
using Mirror;
using Player;
using States.Menus;
using UnityEngine;
using UnityEngine.Serialization;

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
        [HideInInspector] public List<int> diceNumbers = new List<int>();

        // Data
        [SyncVar] [HideInInspector] public PlayerData playerData;
        [SyncVar] public bool startGame = false;

        public void Start()
        {
            // Start the game flow
            currentState = new CharacterSetupState(this);
            SetState(currentState);
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

        public void SetStartingValues() // config gamesettings
        {
            playerData = PlayerDataManager.Instance.CmdGetPlayerDataFromIndex((int) PlayerEnum.Player1);
            StartGame();
        }

        [ClientRpc]
        private void StartGame()
        {
            //UIManager.Instance.SetupPlayerCoinsUI(PlayerDataManager.Instance.CmdGetAllPlayerData());
            TurnManager.Instance.SetTotalPlayers(PlayerDataManager.Instance.CmdGetTotalPlayers());
            TurnManager.Instance.SetTotalTurns(10);
            startGame = true;
        }


        public bool IsPlayerTurn()
        {
            return NetworkHelpers.PlayerHasAuthority(TurnManager.Instance.CmdGetCurrentPlayer());
        }
    }
}