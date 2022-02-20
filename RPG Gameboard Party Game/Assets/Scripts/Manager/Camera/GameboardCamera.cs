using System.Diagnostics.CodeAnalysis;
using Manager.Player;
using Manager.Turns;
using Mirror;
using UnityEngine;

namespace Manager.Camera
{
    public class GameboardCamera : NetworkBehaviour
    {
        public GameObject gameboardCamera;
        [SyncVar] [HideInInspector] public GameObject cameraInScene;


        [Command(requiresAuthority = false)]
        [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
        public void CmdEnableGameBoardCamera()
        {
            // create game board camera on the server
            var boardCam = Instantiate(gameboardCamera, gameboardCamera.transform.position,
                gameboardCamera.transform.rotation);

            var player = PlayerDataManager.Instance.clientPlayerData[TurnManager.Instance.currentPlayerTurnOrder]
                .Identity;

            // Move to player location
            boardCam.transform.position = new Vector3(player.gameObject.transform.position.x,
                boardCam.transform.position.y, player.gameObject.transform.position.z);

            NetworkServer.Spawn(boardCam, player.connectionToClient);

            // Set this here, so we have a public reference we can call server remove on later.
            cameraInScene = boardCam;
        }

        [Command(requiresAuthority = false)]
        public void CmdDisableGameBoardCamera()
        {
            RpcDisableGameBoardCamera();
        }

        [ClientRpc]
        private void RpcDisableGameBoardCamera()
        {
            if (cameraInScene != null)
            {
                NetworkServer.Destroy(cameraInScene);
            }
        }
    }
}