using System.Collections;
using Game.States.GameBoard;
using Game.States.GameBoard.StateSystem;
using Manager.Player;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerWaitingState : GameStates
    {
        private int _playerId;

        public PlayerWaitingState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            gameSystem.IsPlayerTurnResetDebug();
            LogPlayerTurn(false);
            yield return null;
        }

        public override void Tick()
        {
            if (gameSystem.IsPlayerTurn())
            {
                GoToTurnStart();
            }
        }

        private void GoToTurnStart()
        {
            LogPlayerTurn(true);
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerStartTurnState(gameSystem)));
        }

        [ClientRpc]
        private void LogPlayerTurn(bool isTurn)
        {
            Debug.Log(!isTurn ? $"Player {gameSystem.playerId} Waiting For Turn" : $"Player {gameSystem.playerId} Starting Turn");
        }
    }
}