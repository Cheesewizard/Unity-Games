using System.Collections;
using System.Collections.Generic;
using Game.GameBoard;
using Game.States.Menus;
using Manager.Camera;
using Manager.Player;
using Manager.Turns;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.GameBoard.StateSystem
{
    public class GameBoardSystem : StateMachine
    {
        // Game Board
        public Route gameBoard;
        [HideInInspector] public GameObject currentTile;

        // Data
        [SyncVar] [HideInInspector] public bool startGame = false;

        public int playerId;

        // Camera
        public PlayerCameraManager playerCamera;

        public void StartGame()
        {
            gameBoard = FindObjectOfType<Route>();

            // Start the game flow
            currentState = new CharacterSetupState(this);
            SetState(currentState);
        }

        void Update()
        {
            currentState?.Tick();
        }

        public IEnumerator TransitionToState(float timeToWait, GameStates state)
        {
            yield return new WaitForSeconds(timeToWait);
            SetState(state);
        }

        public void SetStartingValues() // config gamesettings
        {
            SetGameData();
        }

        private void SetGameData()
        {
            //UIManager.Instance.SetupPlayerCoinsUI(PlayerDataManager.Instance.CmdGetAllPlayerData());
            TurnManager.Instance.SetTotalPlayers(PlayerDataManager.Instance.clientPlayerDataDict.Count);
            TurnManager.Instance.SetTotalTurns(10);
            startGame = true;
        }

        public bool IsPlayerTurn(int id)
        {
            return id == TurnManager.Instance.currentPlayerTurnOrder;
        }
    }
}