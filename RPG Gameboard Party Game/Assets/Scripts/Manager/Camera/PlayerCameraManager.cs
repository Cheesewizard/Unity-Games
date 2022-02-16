using Game.Camera;
using Manager.Player;
using Manager.Turns;
using Mirror;
using UnityEngine;

namespace Manager.Camera
{
    public class PlayerCameraManager : NetworkBehaviour
    {
        public GameObject playerCamera;
        public GameObject gameboardCamera;

        private const int _zoomOut = 7;
        private const int _zoomIn = 3;
        private const float _zoomSpeed = 2f;

        [Command(requiresAuthority = false)]
        public void CmdEnablePlayerCamera()
        {
            RpcEnableCamera();
        }

        [Command(requiresAuthority = false)]
        public void CmdDisablePlayerCamera()
        {
            RpcDisableCamera();
        }

        [ClientRpc]
        private void RpcEnableCamera()
        {
            playerCamera.gameObject.SetActive(true);
        }

        [ClientRpc]
        private void RpcDisableCamera()
        {
            playerCamera.gameObject.SetActive(false);
        }


        [Command(requiresAuthority = false)]
        public void CmdEnableGameBoardCamera()
        {
            var boardCam = Instantiate(gameboardCamera, gameboardCamera.transform.position, Quaternion.identity);
            var identity = boardCam.GetComponent<NetworkConnection>().identity;
            NetworkServer.Spawn(identity.gameObject);
            
            //gameboardCamera.GetComponent<MoveTransform>().isEnabled = true;
            RpcEnableGameBoardCamera();
        }

        [Command(requiresAuthority = false)]
        public void CmdDisableGameBoardCamera()
        {
            NetworkServer.Destroy(gameboardCamera);
            RpcDisableGameBoardCamera();
        }

        [ClientRpc]
        private void RpcEnableGameBoardCamera()
        {
            var player = PlayerDataManager.Instance.clientPlayerDataDict[TurnManager.Instance.currentPlayerTurnOrder]
                .Identity.gameObject;

            gameboardCamera.transform.position = new Vector3(player.transform.position.x,
                gameboardCamera.transform.position.y, player.transform.position.z);

            gameboardCamera.gameObject.SetActive(true);
        }

        [ClientRpc]
        private void RpcDisableGameBoardCamera()
        {
            gameboardCamera.gameObject.SetActive(false);
        }

        [Command(requiresAuthority = false)]
        public void CmdZoomOut()
        {
            RpcZoomOut();
        }

        [ClientRpc]
        private void RpcZoomOut()
        {
            var followPlayer = playerCamera.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                    new Vector3(followPlayer.offset.x, _zoomOut, followPlayer.offset.z), _zoomSpeed * Time.deltaTime);
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdZoomIn()
        {
            RpcZoomIn();
        }

        [ClientRpc]
        private void RpcZoomIn()
        {
            var followPlayer = playerCamera.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                    new Vector3(followPlayer.offset.x, _zoomIn, followPlayer.offset.z), _zoomSpeed * Time.deltaTime);
            }
        }
    }
}