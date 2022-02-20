using Game.Camera;
using Helpers;
using Manager.Player;
using Manager.Turns;
using Mirror;
using UnityEngine;

namespace Manager.Camera
{
    public class PlayerCamera : NetworkBehaviour
    {
        public GameObject playerCamera;
        
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