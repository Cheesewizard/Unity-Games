using System.Collections;
using Camera;
using Manager.Camera;
using Manager.Dice;
using Manager.Player;
using Manager.Turns;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.GameBoard
{
    public class EndTurnState : GameStates
    {
        public EndTurnState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered End Turn");
            IncreaseTurn();
            
            gameSystem.playerData =
                PlayerDataManager.Instance.CmdGetPlayerDataFromIndex((int) TurnManager.Instance.CmdGetCurrentPlayer());

            // Clunky implementation
            GameStates currentState = new StartTurnState(gameSystem);
            if (gameSystem.playerData.IsPlayerTurnStarted)
            {
                currentState = new GameSetupState(gameSystem);
            }

            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, currentState));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            DiceManager.Instance.RemoveDiceFromScene();

            // Move camera to next player
            CameraManager.Instance.CmdChangeCameraTargetPlayer(CameraEnum.PlayerCamera,
                gameSystem.playerData.Player.transform);
            yield return null;
        }

        [Command]
        private void IncreaseTurn()
        {
            TurnManager.Instance.CmdIncrementTurnOrder();
        }
    }
}