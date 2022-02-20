using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using Helpers;
using Mirror;
using UnityEngine;

namespace Game.States.GameBoard
{
    public class GameBoardCameraState : GameStates
    {
        public GameBoardCameraState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered looking At GameBoard");
            gameSystem.gameBoardCamera.CmdEnableGameBoardCamera();
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
        }


        [Client]
        private void CheckInput()
        {
            CmdExitCameraButton();
        }

        [Command]
        private void CmdExitCameraButton()
        {
            // Press T to continue
            if (Input.GetKeyDown(KeyCode.T))
            {
                GoToPlayerState();
            }
        }
        
        private void GoToPlayerState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerState(gameSystem)));
            gameSystem.gameBoardCamera.CmdDisableGameBoardCamera();
        }
    }
}