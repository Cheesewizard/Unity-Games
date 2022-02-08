using System.Collections;
using Camera;
using Helpers;
using Manager.Camera;
using Manager.Player;
using Manager.Turns;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.GameBoard
{
    public class GameBoardCameraState : GameStates
    {
        public GameBoardCameraState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered looking At GameBoard");
            EnableMoveCamera(true);

            CameraManager.Instance.RpcMoveCameraPositionTo(CameraEnum.GameBoardCamera,
                PlayerDataManager.Instance.CmdGetPlayerDataFromIndex((int)TurnManager.Instance.CmdGetCurrentPlayer())
                    .Player
                    .gameObject.transform);

            CameraManager.Instance.RpcEnableGameBoardCamera();
            yield return null;
        }

        public override IEnumerator Exit()
        {
            EnableMoveCamera(false);
            yield return null;
        }

        public override void Tick()
        {
            if (gameSystem.IsPlayerTurn())
            {
                CheckInput();
            }
        }

        [ClientRpc]
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

        [ClientRpc]
        private void GoToPlayerState()
        {
            CameraManager.Instance.RpcEnablePlayerCamera();
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Player.PlayerState(gameSystem)));
        }
    }
}