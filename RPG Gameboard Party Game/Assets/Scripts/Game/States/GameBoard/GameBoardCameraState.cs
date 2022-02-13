using System.Collections;
using Camera;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using Helpers;
using Manager.Camera;
using Manager.Player;
using Mirror;
using States.GameBoard.StateSystem;
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
            EnableMoveCamera(true);
            CameraManager.Instance.CmdMoveCameraPositionTo(CameraEnum.GameBoardCamera,PlayerDataManager.Instance.currentPlayerData.networkInstanceId);
            CameraManager.Instance.CmdEnableGameBoardCamera();
            yield return null;
        }

        public override IEnumerator Exit()
        {
            EnableMoveCamera(false);
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
        }

        [Command]
        private void EnableMoveCamera(bool isEnable)
        {
            CameraManager.Instance.camerasInScene[CameraEnum.GameBoardCamera].gameObject
                .GetComponent<MoveTransform>().isEnabled = isEnable;
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
            CameraManager.Instance.CmdEnablePlayerCamera();
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerState(gameSystem)));
        }
    }
}