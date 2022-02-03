using UnityEngine;

namespace States.Player
{
    public class MovePlayerToTile : MonoBehaviour
    {
        public bool MoveToNextNode(Vector3 target, int speed)
        {
            return target != (transform.position =
                UnityEngine.Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime));
        }
    }
}