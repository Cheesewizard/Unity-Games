using UnityEngine;

namespace Helpers
{
    public class MoveTransform : MonoBehaviour
    {
        public float offset = 2f; 
        public float speed = 2f;
        public bool isEnabled = false;

        void LateUpdate()
        {
            if (isEnabled)
            {
                Move();
            }
        }

        private void Move()
        {
            var movement = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += movement * speed * Time.deltaTime;
        }
    }
}