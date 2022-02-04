using System.Collections;
using Helpers;
using States.GameBoard;
using UnityEngine;

namespace States.GameBoard
{
    public class LookAtGameBoard: GameStates
    {
        public LookAtGameBoard(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered looking At GameBoard");
            gameSystem.cameraManager.MoveCameraToPlayerInstant(CameraEnum.PlayerCamera, gameSystem.playerData.Player.transform);
            gameSystem.cameraManager.EnableGameBoardCamera();
            gameSystem.cameras[(int) CameraEnum.GameBoardCamera].gameObject.GetComponent<MoveTransform>().isEnabled = true;
            
            yield return null;
        }

        public override IEnumerator Exit()
        {
            gameSystem.cameras[(int) CameraEnum.GameBoardCamera].gameObject.GetComponent<MoveTransform>().isEnabled = false;
            yield return null;
        }

        public override void Tick()
        {
            // Press T to continue
            if (Input.GetKeyDown(KeyCode.T))
            {
                gameSystem.cameraManager.EnablePlayerCamera();
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Player.Player(gameSystem)));
            } 
        }
    }
}