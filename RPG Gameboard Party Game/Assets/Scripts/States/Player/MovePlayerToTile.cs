using UnityEngine;

namespace States.Player
{
    public class MovePlayerToTile : MonoBehaviour
    {
        public bool MoveToNextNode(Vector3 target, int speed)
        {
            var newPosition = target;
            var smoothPosition = Vector3.Lerp(transform.position, newPosition, 0.5f);
            
            return target != (transform.position =
                UnityEngine.Vector3.MoveTowards(transform.position, smoothPosition, speed * Time.deltaTime));
        }
    }
}