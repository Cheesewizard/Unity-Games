using System;
using System.Collections.Generic;
using Game.Player;
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

        private void Start()
        {
            serverPlayerDataDict.Callback += UpdateClientPlayerData;
        }

        private void UpdateClientPlayerData(SyncIDictionary<int, PlayerData>.Operation op, int key, PlayerData item)
        {
            foreach (var player in serverPlayerDataDict)
            {
                // Force re-update local player list
                if (clientPlayerDataDict.ContainsKey(player.Key))
                {
                    clientPlayerDataDict[player.Key] = player.Value;
                }
                // Add player to local player list
                else
                {
                    clientPlayerDataDict.Add(player.Key, player.Value);
                }

            }
        }

        [SyncVar] public int totalPlayers;
        public readonly SyncDictionary<int, PlayerData> serverPlayerDataDict = new SyncDictionary<int, PlayerData>();
        public readonly Dictionary<int, PlayerData> clientPlayerDataDict = new Dictionary<int, PlayerData>();

        [Command(requiresAuthority = false)]
        public void CmdAddPlayerToServer(PlayerData player)
        {
            if (serverPlayerDataDict.ContainsKey(player.playerId))
            {
                return;
            }

            serverPlayerDataDict.Add(player.playerId, player);
        }

        [Command(requiresAuthority = false)]
        public void CmdRemovePlayer(int playerId)
        {
            if (serverPlayerDataDict.ContainsKey(playerId))
            {
                serverPlayerDataDict.Remove(playerId);
            }
        }

        // This may not sync the dictionary to other clients. needs testing.

        [Command(requiresAuthority = false)]
        public void CmdUpdatePlayerData(int playerId, PlayerData player)
        {
            if (serverPlayerDataDict.ContainsKey(playerId))
            {
                serverPlayerDataDict[playerId] = player;
            }
        }
    }
}