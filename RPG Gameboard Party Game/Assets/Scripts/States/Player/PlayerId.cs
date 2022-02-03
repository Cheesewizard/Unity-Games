using UnityEngine;

namespace States.Player
{
    public class PlayerId : MonoBehaviour, IPlayer
    {
        [SerializeField]
        public int Id { get; set; }

        public void SetPlayerId(int id)
        {
            this.Id = id;
        }
    }
}