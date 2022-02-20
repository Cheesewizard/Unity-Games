using System.Collections.Generic;
using Game.GameBoard.Items;
using Manager.UI;
using Mirror;
using ScriptableObject.Money;
using UnityEngine;

namespace Manager.Money
{
    public class MoneyManager : NetworkBehaviour
    {
        public static MoneyManager Instance { get; private set; }

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


        public readonly SyncDictionary<int, int> serverMoney = new SyncDictionary<int, int>();
        public readonly Dictionary<int, int> clientMoney = new Dictionary<int, int>();

        private void OnEnable()
        {
            serverMoney.Callback += OnUpdatePlayerMoney;
        }

        private void OnDisable()
        {
            serverMoney.Callback -= OnUpdatePlayerMoney;
        }

        private void OnUpdatePlayerMoney(SyncIDictionary<int, int>.Operation op, int key,
            int item)
        {
            foreach (var player in serverMoney)
            {
                // Force re-update of player money
                if (clientMoney.ContainsKey(player.Key))
                {
                    clientMoney[player.Key] = player.Value;
                    PlayerUIManager.Instance.CmdUpdatePlayerMoney(key,item);
                }
                // Add money for player
                else
                {
                    clientMoney.Add(player.Key, player.Value);
                    PlayerUIManager.Instance.CmdUpdatePlayerMoney(key,item);
                }
            }
        }
        

        [Command(requiresAuthority = false)]
        public void CmdPlayerMoneySetup(int playerId)
        {
            if (serverMoney.ContainsKey(playerId))
            {
                return;
            }

            serverMoney.Add(playerId, 0);
        }

        [Command(requiresAuthority = false)]
        public void CmdAddMoney(int playerId, string itemId)
        {
            var item = ItemDatabase.Instance.GetItem(itemId);
            if (item != null)
            {
                var money = (MoneyObject) item;
                serverMoney[playerId] += money.value;
                Debug.Log($"Player {playerId} added {money.value} gold");
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdRemoveMoney(int playerId, string itemId)
        {
            var item = ItemDatabase.Instance.GetItem(itemId);
            if (item != null)
            {
                var money = (MoneyObject) item;
                serverMoney[playerId] -= money.value;
                Debug.Log($"Player {playerId} lost {money.value} gold");
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdUpdateMoney()
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