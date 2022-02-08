using System.Collections;
using Camera;
using Helpers;
using Manager.Camera;
using Manager.Dice;
using Manager.Player;
using Mirror;
using Player;
using States.GameBoard.StateSystem;
using States.Player;
using UnityEngine;


namespace States.GameBoard
{
    public class GameSetupState : GameStates
    {
        private MoveToTarget _movePosition;

        public GameSetupState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            gameSystem.diceNumbers.Add(1);
            Debug.Log("Entered Start Game");
            
            // Set camera to player 1 on start
            CameraManager.Instance.CmdChangeCameraTargetPlayer(CameraEnum.PlayerCamera,
                gameSystem.playerData.Player.transform);
            
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMove(gameSystem)));
            yield return null;
        }
    }
}