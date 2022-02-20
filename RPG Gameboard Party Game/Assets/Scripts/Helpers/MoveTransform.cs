using Mirror;
using UnityEngine;

namespace Helpers
{
    public class MoveTransform : NetworkBehaviour
    {
        public float speed = 2f;
        private Vector3 _lastMovement;

        void Update()
        {
            if (hasAuthority)
                GetMovement();
        }

        private void GetMovement()
        {
            var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            CmdMoveCamera(movement);
        }

        [Command]
        private void CmdMoveCamera(Vector3 movement)
        {
            RpcMoveCamera(movement);
        }

        [ClientRpc]
        private void RpcMoveCamera(Vector3 movement)
        {
            transform.position += movement * speed * Time.deltaTime;
        }
    }
}