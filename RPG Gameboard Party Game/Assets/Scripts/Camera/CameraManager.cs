using System;
using UnityEngine;

namespace Camera
{
    public class CameraManager
    {
        private GameObject[] _cameras;
        private GameObject _startTarget;

        private const int _zoomOut = 7;
        private const int _zoomIn = 3;
        private const float _zoomSpeed = 2f;

        public CameraManager(GameObject[] cameras)
        {
            _cameras = cameras;
        }

        public void MoveCameraToPlayerInstant(CameraEnum camera, Transform target)
        {
            var followPlayer = _cameras[(int) camera].gameObject.GetComponent<FollowPlayer>();
            followPlayer.target = target;
        } 
        
        [Obsolete]
        public void MoveCameraToPlayerSmooth(CameraEnum camera, Transform target)
        { 
            var followPlayer = _cameras[(int) camera].gameObject.GetComponent<FollowPlayer>();
            
            followPlayer.target.position = Vector3.Lerp(followPlayer.transform.position,
                target.position, _zoomSpeed * Time.deltaTime);
        }

        public void EnablePlayerCamera()
        {
            _cameras?[(int) CameraEnum.PlayerCamera].gameObject.SetActive(true);
            _cameras?[(int) CameraEnum.GameBoardCamera].gameObject.SetActive(false);
        }

        public void EnableGameBoardCamera()
        {
            _cameras?[(int) CameraEnum.GameBoardCamera].gameObject.SetActive(true);
            _cameras?[(int) CameraEnum.PlayerCamera].gameObject.SetActive(false);
        }

        public void ZoomOut()
        {
            var followPlayer = _cameras?[(int) CameraEnum.PlayerCamera].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                    new Vector3(followPlayer.offset.x, _zoomOut, followPlayer.offset.z), _zoomSpeed * Time.deltaTime);
            }
        }

        public void ZoomIn()
        {
            var followPlayer = _cameras?[(int) CameraEnum.PlayerCamera].gameObject.GetComponent<FollowPlayer>();
            if (followPlayer != null)
            {
                // followPlayer.offset = new Vector3(followPlayer.offset.x,
                //     Mathf.Lerp(_zoomOut, _zoomIn, _zoomSpeed * Time.deltaTime), followPlayer.offset.z);
                
                followPlayer.offset = Vector3.Lerp(followPlayer.offset,
                    new Vector3(followPlayer.offset.x, _zoomIn, followPlayer.offset.z), _zoomSpeed * Time.deltaTime);
            }
        }
    }
}