using System;
using System.Collections.Generic;
using Camera;
using Helpers;
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

        public GameObject[] cameras;
        public List<GameObject> camerasInScene = new List<GameObject>();
        private GameObject _startTarget;

        private const int _zoomOut = 7;
        private const int _zoomIn = 3;
        private const float _zoomSpeed = 2f;


        [ClientRpc]
        public void RpcSpawnCamerasIntoScene()
        {
            // Temp skip player camera
            for (var i = 1; i < cameras.Length; i++)
            {
                var cam = Instantiate(cameras[i], cameras[i].transform.position, cameras[i].transform.rotation);
                NetworkServer.Spawn(cam);

                if (i == (int) CameraEnum.GameBoardCamera)
                {
                    NetworkHelpers.RpcToggleObjectVisibility(cam, false);
                }

                camerasInScene.Add(cam);
            }

            // Temp add the scene camera into our scene cameras list
            camerasInScene.Insert(0, cameras[(int) CameraEnum.PlayerCamera]);
        }

        [ClientRpc]
        public void RpcChangeCameraTargetPlayer(CameraEnum camera, Transform target)
        {
            var followPlayer = camerasInScene[(int) camera].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.target = target;
            }
        }

        [ClientRpc]
        public void RpcMoveCameraPositionTo(CameraEnum camera, Transform target)
        {
            var newPosition = new Vector3(target.transform.position.x,
                camerasInScene[(int) camera].gameObject.transform.position.y, target.transform.position.z);

            camerasInScene[(int) camera].gameObject.transform.position = newPosition;
        }

        [Obsolete]
        public void MoveCameraToPlayerSmooth(CameraEnum camera, Transform target)
        {
            var followPlayer = camerasInScene[(int) camera].gameObject.GetComponent<FollowPlayer>();

            followPlayer.target.position = Vector3.Lerp(followPlayer.transform.position,
                target.position, _zoomSpeed * Time.deltaTime);
        }

        [ClientRpc]
        public void RpcEnablePlayerCamera()
        {
            camerasInScene?[(int) CameraEnum.PlayerCamera].gameObject.SetActive(true);
            camerasInScene?[(int) CameraEnum.GameBoardCamera].gameObject.SetActive(false);
        }

        [ClientRpc]
        public void RpcEnableGameBoardCamera()
        {
            camerasInScene?[(int) CameraEnum.GameBoardCamera].gameObject.SetActive(true);
            camerasInScene?[(int) CameraEnum.PlayerCamera].gameObject.SetActive(false);
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