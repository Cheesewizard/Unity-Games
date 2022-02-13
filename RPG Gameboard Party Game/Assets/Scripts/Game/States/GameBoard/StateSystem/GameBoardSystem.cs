using System.Collections;
using System.Collections.Generic;
using Game.GameBoard;
using Game.States.Menus;
using Manager.Player;
using Manager.Turns;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.States.GameBoard.StateSystem
{
    public class GameBoardSystem : StateMachine
    {
        // Game Board
        public Route gameBoard;
        [HideInInspector] public GameObject currentTile;

        // Dice
        [HideInInspector] public List<int> diceNumbers = new List<int>();

        // Data
        [SyncVar] [HideInInspector] public bool startGame = false;
        public int playerId;

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
            TurnManager.Instance.SetTotalPlayers(PlayerDataManager.Instance.totalPlayers);
            TurnManager.Instance.SetTotalTurns(10);
            startGame = true;
        }


        [SyncVar]
        private int _prevPlayerId = -1;

        [Client]
        public bool IsPlayerTurn()
        {
            var player = PlayerDataManager.Instance.currentPlayerData;

            var playerTurn = player.playerId == TurnManager.Instance.currentPlayerTurnOrder;
            if (playerTurn && player.playerId != _prevPlayerId)
            {
                _prevPlayerId = player.playerId;
                return true;
            }

            return false;
        }

        public void IsPlayerTurnResetDebug()
        {
            _prevPlayerId = -1;
        }
    }
}