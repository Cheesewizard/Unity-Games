using System.Collections.Generic;
using Camera;
using Game.Camera;
using Manager.Player;
using Mirror;
using UnityEngine;

namespace Manager.Camera
{
    public class CameraManager : NetworkBehaviour
    {
        public static CameraManager Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public readonly Dictionary<CameraEnum, GameObject> camerasInScene = new Dictionary<CameraEnum, GameObject>();
        public GameObject[] camerasReferences;
        private GameObject _startTarget;

        private const int _zoomOut = 7;
        private const int _zoomIn = 3;
        private const float _zoomSpeed = 2f;
        
       
        [Client]
        public void Start()
        {
            // Could be bad practice?
            //var cameras = GameObject.FindGameObjectsWithTag("Camera");
            if (camerasReferences == null) return; 

            foreach (var cam in camerasReferences)
            {
                var type = cam.GetComponent<CameraValue>();
                if (type == null) return;

                camerasInScene.Add(type.cameraType, cam);
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdChangeCameraTargetPlayer(CameraEnum cameraType, uint networkId)
        {
            var target = PlayerDataManager.Instance.currentPlayerData.Identity.gameObject;
            RpcChangeCameraTargetPlayer(cameraType, target);
        }

        [ClientRpc]
        private void RpcChangeCameraTargetPlayer(CameraEnum cameraType, GameObject target)
        {
            var followPlayer = camerasInScene[cameraType].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.target = target.transform;
            }
        }

        [Command (requiresAuthority = false)]
        public void CmdMoveCameraPositionTo(CameraEnum cameraType, uint networkId)
        { 
            var target = PlayerDataManager.Instance.currentPlayerData.Identity.gameObject;
            RpcMoveCameraPositionTo(cameraType, target);
        }

        [ClientRpc]
        private void RpcMoveCameraPositionTo(CameraEnum cameraType, GameObject target)
        {
            var targetPosition = target.transform.position;
            var newPosition = new Vector3(targetPosition.x,camerasInScene[cameraType].transform.position.y, targetPosition.z);
            camerasInScene[cameraType].transform.position = newPosition;
        }
        
        [Command (requiresAuthority = false)]
        public void CmdEnablePlayerCamera()
        {
            RpcEnablePlayerCamera();
        }

        [ClientRpc]
        private void RpcEnablePlayerCamera()
        {
            camerasInScene?[CameraEnum.PlayerCamera].gameObject.SetActive(true);
            camerasInScene?[CameraEnum.GameBoardCamera].gameObject.SetActive(false);
        }

        [Command (requiresAuthority = false)]
        public void CmdEnableGameBoardCamera()
        {
            RpcEnableGameBoardCamera();
        }

        [ClientRpc]
        private void RpcEnableGameBoardCamera()
        {
            camerasInScene?[CameraEnum.GameBoardCamera].gameObject.SetActive(true);
            camerasInScene?[CameraEnum.PlayerCamera].gameObject.SetActive(false);
        }

        [Command (requiresAuthority = false)]
        public void CmdZoomOut()
        {
            RpcZoomOut();
        }

        [ClientRpc]
        private void RpcZoomOut()
        {
            var followPlayer = camerasInScene?[(int) CameraEnum.PlayerCamera].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                    new Vector3(followPlayer.offset.x, _zoomOut, followPlayer.offset.z), _zoomSpeed * Time.deltaTime);
            }
        }
        
        [Command (requiresAuthority = false)]
        public void CmdZoomIn()
        {
            RpcZoomIn();
        }

        [ClientRpc]
        private void RpcZoomIn()
        {
            var followPlayer = camerasInScene?[(int) CameraEnum.PlayerCamera].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                    new Vector3(followPlayer.offset.x, _zoomIn, followPlayer.offset.z), _zoomSpeed * Time.deltaTime);
            }
        }
    }
}