using System.Collections;
using Game.States.GameBoard.StateSystem;
using Mirror;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerWaitingState : GameStates
    { 
        private bool _isPlayerTurn;
        
        public PlayerWaitingState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            LogPlayerTurn(false);
            gameSystem.playerCamera.CmdDisablePlayerCamera();
            yield return null;
        }
        
        public override void Tick()
        {
            if (!gameSystem.IsPlayerTurn(gameSystem.playerId) || _isPlayerTurn) return;
            
            // Set camera to this persons turn on start
            gameSystem.playerCamera.CmdEnablePlayerCamera();
            _isPlayerTurn = true;
            GoToTurnStart();
        }

        private void GoToTurnStart()
        {
            LogPlayerTurn(true);
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerStartTurnState(gameSystem)));
        }

        [ClientRpc]
        private void LogPlayerTurn(bool isTurn)
        {
            Debug.Log(!isTurn
                ? $"Player {gameSystem.playerId} Waiting For Turn"
                : $"Player {gameSystem.playerId} Starting Turn");
        }
    }
}