using System.Collections;
using Camera;
using Game.States.GameBoard.StateSystem;
using Manager.Camera;
using Manager.Player;
using Manager.Turns;
using States.GameBoard.StateSystem;
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
            while (!WaitUntilPlayerTurn())
            {
                yield return new WaitForSeconds(5);
            }
            
            //Set camera to this persons turn on start
            CameraManager.Instance.CmdChangeCameraTargetPlayer(CameraEnum.PlayerCamera,
                PlayerDataManager.Instance.currentPlayerData.networkInstanceId);
            
            Debug.Log($"Player {gameSystem.playerId} Starting Game Setup State");
            gameSystem.diceNumbers.Add(1);
           
            GoToMovePlayerOntoBoard();
        }

        private bool WaitUntilPlayerTurn()
        { 
            var player = PlayerDataManager.Instance.currentPlayerData;
            return player.playerId == TurnManager.Instance.currentPlayerTurnOrder;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        private void GoToMovePlayerOntoBoard()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMoveOntoBoardState(gameSystem)));
        }
    }
}