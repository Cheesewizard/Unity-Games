using Mirror;
using Player.Inventory;
using UnityEngine;

namespace Player
{
    public struct PlayerData
    {
        public NetworkIdentity NetworkIdentity { get; set; }
        public GameObject Player { get; set; }
        public int BoardLocationIndex { get; set; }
        public bool IsPlayerTurnStarted { get; set; }

        public int PlayerTurnId { get; set; }

        public int PlayerTurnCounter { get; set; }
        public IInventory Inventory { get; set; }

        public int PlayerSpeed { get; set; }
    }
}