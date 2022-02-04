using States.Player.Inventory;
using UnityEngine;

namespace States.Player
{
    public struct PlayerData
    {
        public int PlayerId { get; set; }
        public GameObject Player { get; set; }
        public int PositionIndex { get; set; }
        public IInventory Inventory { get; set; }
    }
}