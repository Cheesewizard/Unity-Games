using System.Collections.Generic;
using Game.GameBoard.Items;
using Mirror;
using ScriptableObject.Inventory;
using UnityEngine;

namespace Manager.Inventory
{
    public class InventoryManager : NetworkBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }


        public readonly SyncDictionary<int, InventoryObject> serverInventory =
            new SyncDictionary<int, InventoryObject>();

        public readonly Dictionary<int, InventoryObject> clientInventory = new Dictionary<int, InventoryObject>();

        private void OnEnable()
        {
            serverInventory.Callback += OnUpdatePlayerInventory;
        }

        private void OnDisable()
        {
            serverInventory.Callback -= OnUpdatePlayerInventory;
        }

        private void OnUpdatePlayerInventory(SyncIDictionary<int, InventoryObject>.Operation op, int key,
            InventoryObject item)
        {
            foreach (var player in serverInventory)
            {
                // Force re-update of player inventory
                if (clientInventory.ContainsKey(player.Key))
                {
                    clientInventory[player.Key] = player.Value;
                }
                // Add inventory for player
                else
                {
                    clientInventory.Add(player.Key, player.Value);
                }
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdSetupPlayerInventory(int playerId)
        {
            if (serverInventory.ContainsKey(playerId))
            {
                return;
            }

            serverInventory.Add(playerId, UnityEngine.ScriptableObject.CreateInstance<InventoryObject>());
        }


        [Command(requiresAuthority = false)]
        public void CmdAddToInventory(int playerId, string itemId, int amount)
        {
            var item = ItemDatabase.Instance.GetItem(itemId);
            if (item != null)
            {
                serverInventory[playerId].AddToInventory(item, amount);
                Debug.Log($"Player {playerId} added item {item.name} to their inventory");
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdRemoveFromInventory(int playerId, string itemId, int amount)
        {
            var item = ItemDatabase.Instance.GetItem(itemId);
            if (item != null)
            {
                serverInventory[playerId].RemoveFromInventory(item, amount);
                Debug.Log($"Player {playerId} removed item {item.name} to their inventory");
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdUpdateInventory()
        {
        }

        // Clear the scriptable object since they retrain data between plays
        private void OnApplicationQuit()
        {
            // serverInventory.Clear();
            // clientInventory.Clear();
        }
    }
}