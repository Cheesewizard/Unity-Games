using Game.States.GameBoard.StateSystem;
using Mirror;
using UnityEngine;

namespace Helpers
{
    public class MoveTransform : NetworkBehaviour
    {
        public float speed = 2f;
        public bool isEnabled = false;


        private Vector3 _lastMovement;

        void Update()
        {
            GetMovement();
        }
        
        private void GetMovement()
        {
            var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Only update the server when we have actually made a change
            if (movement == _lastMovement)
            {
                return;
            }

            _lastMovement = movement;
            MoveCamera(movement);
        }
        
        private void MoveCamera(Vector3 movement)
        {
            transform.position += movement * speed * Time.deltaTime;
        }
    }
}