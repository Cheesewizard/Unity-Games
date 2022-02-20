using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using Manager;
using Manager.Movement;
using Mirror;
using UnityEngine;

namespace Game.States.GameBoard
{
    public class GameSetupState : GameStates
    {
        public GameSetupState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered Game Setup State (Waiting)");
            gameSystem.StartCoroutine(Wait());
            yield return null;
        }

        private IEnumerator Wait()
        {
            while (!gameSystem.IsPlayerTurn(gameSystem.playerId))
            {
                yield return new WaitForSeconds(1);
            }
            
            Debug.Log($"Player {gameSystem.playerId} Starting Game Setup State");
            AddToMovement(1);
            GoToMovePlayerOntoBoard();
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }
        
        private void AddToMovement(int steps)
        {
            MovementManager.Instance.CmdAddMovementAmount(steps);
        }

        private void GoToMovePlayerOntoBoard()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerMoveOntoBoardState(gameSystem)));
        }
    }
}