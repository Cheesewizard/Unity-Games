using System;
using System.Linq;
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
            _playerDataDict.Callback += CmdRefreshTotalPlayers;
        }

        public PlayerData currentPlayerData;

        [SyncVar] public int totalPlayers;
        private readonly SyncDictionary<int, PlayerData> _playerDataDict = new SyncDictionary<int, PlayerData>();

        [Command(requiresAuthority = false)]
        public void CmdAddPlayerToServer(PlayerData player)
        {
            if (_playerDataDict.ContainsKey(player.playerId))
            {
                return;
            }

            _playerDataDict.Add(player.playerId, player);
        }

        [Command(requiresAuthority = false)]
        public void CmdRemovePlayer(int playerId)
        {
            if (_playerDataDict.ContainsKey(playerId))
            {
                _playerDataDict.Remove(playerId);
            }
        }

        // This may not sync the dictionary to other clients. needs testing.

        [Command(requiresAuthority = false)]
        public void CmdUpdatePlayerData(int playerId, PlayerData data)
        {
            if (_playerDataDict.ContainsKey(playerId))
            {
                _playerDataDict[playerId] = data;
                currentPlayerData = data;
            }
        }

        [Command(requiresAuthority = false)]
        public void CmdSetPlayerData(NetworkIdentity identity, int index)
        {
            TargetSetPlayerData(identity.connectionToClient, index);
        }

        [TargetRpc]
        private void TargetSetPlayerData(NetworkConnection conn, int index)
        {
            if (_playerDataDict.Count == 0)
            {
                throw new Exception("Player data manager does not contain any data or the index doesnt exist");
            }

            currentPlayerData = _playerDataDict.ElementAt(index).Value;
        }

        [Client]
        public void SetLocalPlayerData(PlayerData player)
        {
            currentPlayerData = player;
        }

        private void CmdRefreshTotalPlayers(SyncDictionary<int, PlayerData>.Operation op, int key, PlayerData item)
        {
            totalPlayers = _playerDataDict.Count;
        }

        public int GetPlayerNumber()
        {
            // Add one because internally we start with player 0;
            return currentPlayerData.playerId + 1;
        }
    }
}