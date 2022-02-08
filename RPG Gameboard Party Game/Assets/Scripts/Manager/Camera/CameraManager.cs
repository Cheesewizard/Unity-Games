using System;
using System.Collections.Generic;
using Camera;
using Helpers;
using Mirror;
using States.GameBoard.StateSystem;
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

        public Dictionary<CameraEnum, GameObject> camerasInScene = new Dictionary<CameraEnum, GameObject>();
        public GameObject[] camerasReferences;
        private GameObject _startTarget;

        private const int _zoomOut = 7;
        private const int _zoomIn = 3;
        private const float _zoomSpeed = 2f;
        
        public void Start()
        {
            // Could be bad practice?
            //var cameras = GameObject.FindGameObjectsWithTag("Camera");
            if (camerasReferences == null)
            {
                return;
            }

            foreach (var cam in camerasReferences)
            {
                var type = cam.GetComponent<CameraValue>();
                if (type == null)
                {
                    return;
                }
                
                camerasInScene.Add(type.cameraType, cam);
            }
        }

       [Command(requiresAuthority = false)]
        public void CmdChangeCameraTargetPlayer(CameraEnum cameraType, Transform target)
        {
            RpcChangeCameraTargetPlayer(cameraType, target);
        }
        
        [ClientRpc]
        private void RpcChangeCameraTargetPlayer(CameraEnum cameraType, Transform target)
        {
            var followPlayer = camerasInScene[cameraType].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.target = target;
            }
        }

        [Command]
        public void RpcMoveCameraPositionTo(CameraEnum cameraType, Transform target)
        {
            var targetPosition = target.transform.position;
            var newPosition = new Vector3(targetPosition.x,camerasInScene[cameraType].transform.position.y, targetPosition.z);
            camerasInScene[cameraType].transform.position = newPosition;
        }

        [Obsolete]
        public void MoveCameraToPlayerSmooth(CameraEnum cameraType, Transform target)
        {
            var followPlayer = camerasInScene[cameraType].gameObject.GetComponent<FollowPlayer>();

            followPlayer.target.position = Vector3.Lerp(followPlayer.transform.position,
                target.position, _zoomSpeed * Time.deltaTime);
        }

        [ClientRpc]
        public void RpcEnablePlayerCamera()
        {
            camerasInScene?[CameraEnum.PlayerCamera].gameObject.SetActive(true);
            camerasInScene?[CameraEnum.GameBoardCamera].gameObject.SetActive(false);
        }

        [ClientRpc]
        public void RpcEnableGameBoardCamera()
        {
            camerasInScene?[CameraEnum.GameBoardCamera].gameObject.SetActive(true);
            camerasInScene?[CameraEnum.PlayerCamera].gameObject.SetActive(false);
        }

        [ClientRpc]
        public void RpcZoomOut()
        {
            var followPlayer = camerasInScene?[(int) CameraEnum.PlayerCamera].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                    new Vector3(followPlayer.offset.x, _zoomOut, followPlayer.offset.z), _zoomSpeed * Time.deltaTime);
            }
        }

        [ClientRpc]
        public void RpcZoomIn()
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