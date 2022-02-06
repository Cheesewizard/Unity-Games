using System.Collections;
using Manager.Camera;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.GameBoard
{
    public class EndTurn : GameStates
    {
        public EndTurn(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered End Turn");
            gameSystem.turnManager.IncrementTurn();
            gameSystem.playerData = gameSystem.playerDataManager.CmdGetPlayerDataFromIndex(gameSystem.turnManager.GetCurrentPlayerIndex());
            
            // Update authority to next person
            gameSystem.currentAuthority = gameSystem.playerData.NetworkIdentity;
            
            // Clunky implementation
            GameStates currentState = new StartTurn(gameSystem);
            if (gameSystem.playerData.IsPlayerTurnStarted)
            {
                currentState = new StartGame(gameSystem);
            }
            
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, currentState));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            // Move camera to next player
            CameraManager.Instance.RpcChangeCameraTargetPlayer(CameraEnum.PlayerCamera, gameSystem.playerData.Player.transform);
            yield return null;
        }
    }
}