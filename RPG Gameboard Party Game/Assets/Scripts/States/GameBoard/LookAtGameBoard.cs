using System.Collections;
using Helpers;
using Manager.Camera;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.GameBoard
{
    public class LookAtGameBoard : GameStates
    {
        public LookAtGameBoard(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered looking At GameBoard");

            CameraManager.Instance.camerasInScene[(int) CameraEnum.GameBoardCamera].gameObject
                    .GetComponent<MoveTransform>().isEnabled =
                true;

            CameraManager.Instance.RpcMoveCameraPositionTo(CameraEnum.GameBoardCamera,
                gameSystem.playerDataManager.CmdGetPlayerDataFromIndex(gameSystem.turnManager.GetCurrentPlayerIndex())
                    .Player
                    .gameObject.transform);

            CameraManager.Instance.RpcEnableGameBoardCamera();
            yield return null;
        }

        public override IEnumerator Exit()
        {
            CameraManager.Instance.camerasInScene[(int) CameraEnum.GameBoardCamera].gameObject
                    .GetComponent<MoveTransform>().isEnabled =
                false;
            yield return null;
        }

        public override void Tick()
        {
            if (gameSystem.CheckIfHasAuthority())
            {
                CheckInput();
            }
        }

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
                CameraManager.Instance.RpcEnablePlayerCamera();
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Player.Player(gameSystem)));
            }
        }
    }
}