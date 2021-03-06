using UnityEngine;

namespace Game.Camera
{
    public class FollowPlayer : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float smooth = 10f;

        void Update()
        {
            if (target == null)
            {
                return;
            }

            var newPosition = target.position + offset;
            var smoothPosition = Vector3.Lerp(transform.position, newPosition, smooth);

            transform.position = smoothPosition;
            transform.LookAt(target);
        }
    }
}