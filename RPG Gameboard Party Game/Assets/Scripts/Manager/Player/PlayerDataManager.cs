using System;
using System.Collections.Generic;
using Game.Player;
using Manager.UI;
using Mirror;

namespace Manager.Player
{
    public class PlayerDataManager : NetworkBehaviour
    {
        public static PlayerDataManager Instance { get; private set; }

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
        
        public readonly SyncDictionary<int, PlayerData> serverPlayerData = new SyncDictionary<int, PlayerData>();
        public readonly Dictionary<int, PlayerData> clientPlayerData = new Dictionary<int, PlayerData>();
        
        private void OnEnable()
        {
            serverPlayerData.Callback += OnUpdateClientPlayerData;
            serverPlayerData.Callback += OnAddPlayer;
        }

        private void OnDisable()
        {
            serverPlayerData.Callback -= OnUpdateClientPlayerData;
            serverPlayerData.Callback -= OnAddPlayer;
        }

        private void OnAddPlayer(SyncIDictionary<int, PlayerData>.Operation op, int key, PlayerData item)
        {
            if (op == SyncIDictionary<int, PlayerData>.Operation.OP_ADD)
            {
                GameBoardUIManager.Instance.CmdTogglePlayerUIPanels(item);
            }
        }

        private void OnUpdateClientPlayerData(SyncIDictionary<int, PlayerData>.Operation op, int key, PlayerData item)
        {
            foreach (var player in serverPlayerData)
            {
                // Force re-update local player list
                if (clientPlayerData.ContainsKey(player.Key))
                {
                    clientPlayerData[player.Key] = player.Value;
                }
                // Add player to local player list
                else
                {
                    clientPlayerData.Add(player.Key, player.Value);
                }
            }
        }

      

        [Command(requiresAuthority = false)]
        public void CmdAddPlayerToServer(PlayerData player)
        {
            if (serverPlayerData.ContainsKey(player.playerId))
            {
                return;
            }

            serverPlayerData.Add(player.playerId, player);
        }

        [Command(requiresAuthority = false)]
        public void CmdRemovePlayer(int playerId)
        {
            if (serverPlayerData.ContainsKey(playerId))
            {
                serverPlayerData.Remove(playerId);
            }
        }

        // This may not sync the dictionary to other clients. needs testing.

        [Command(requiresAuthority = false)]
        public void CmdUpdatePlayerData(int playerId, PlayerData player)
        {
            if (serverPlayerData.ContainsKey(playerId))
            {
                serverPlayerData[playerId] = player;
            }
        }
    }
}