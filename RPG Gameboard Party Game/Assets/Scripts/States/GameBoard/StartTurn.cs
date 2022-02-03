using System.Collections;
using UnityEngine;

namespace States.GameBoard
{
    public class StartTurn : GameStates
    {
        public StartTurn(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Start Turn");
            Debug.Log("Press Space To Continue");
            
            // 1st ever call is redundant since this is called in the gameboardsystem. Its needed though to setup camera for 1st time
            gameSystem.playerData = gameSystem.playerManager.GetPlayerData(gameSystem.turnManager.CurrentPlayer());

            gameSystem.cameraManager.MoveCameraToPlayer(CameraEnum.PlayerCamera, gameSystem.playerData.Player.transform);
            gameSystem.cameraManager.EnablePlayerCamera();
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            // Press space to continue
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.5f, new Player.Player(gameSystem)));
            } 
        }
    }
}