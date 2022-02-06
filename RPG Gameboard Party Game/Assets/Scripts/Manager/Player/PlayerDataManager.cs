using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Player;
using Player.Inventory;
using UnityEngine;

namespace Manager.Player
{
    public class PlayerDataManager
    {
        private SyncDictionary <uint, PlayerData> _playerDataDict = new SyncDictionary <uint, PlayerData>();

        private List<GameObject> _players;
        private PlayerData _playerData;
        
        [Command]
        public void CmdAddPlayer(NetworkIdentity networkIdentity, GameObject player)
        {
            if (_playerDataDict.ContainsKey(networkIdentity.netId))
            {
                return;
            }

            var data = new PlayerData()
            {
                NetworkIdentity = networkIdentity,
                Player = player,
                PositionIndex = 0,
                Inventory = new Inventory(networkIdentity.netId),
                IsPlayerTurnStarted = true,
                PlayerSpeed = 20
            };

            _playerDataDict.Add(networkIdentity.netId, data);
        }
        
        [Command]
        public void CmdRemovePlayer(uint playerId)
        {
            if (_playerDataDict.ContainsKey(playerId))
            {
                _playerDataDict.Remove(playerId);
            }
        }
        
        [Command]
        public void CmdUpdatePlayerData(uint playerId, PlayerData data)
        {
            if (_playerDataDict.ContainsKey(playerId))
            {
                _playerDataDict[playerId] = data;
            }
        }
        
        [Command]
        public PlayerData CmdGetPlayerData(uint playerId)
        {
            return !_playerDataDict.ContainsKey(playerId) ? new PlayerData() : _playerDataDict[playerId];
        }
        
        [Command]
        public PlayerData CmdGetPlayerDataFromIndex(int index)
        {
            if (_playerDataDict.Count == 0)
            {
                throw new Exception("Player data manager does not contain any data or the index doesnt exist");
            }

            return _playerDataDict.ElementAt(index).Value;
        }
        
        [Command]
        public int CmdGetTotalPlayers()
        {
            return _playerDataDict.Count;
        }
        
        [Command]
        public List<PlayerData> CmdGetAllPlayerData()
        {
            return _playerDataDict.Select((t, i) => _playerDataDict.ElementAt(i).Value).ToList();
        }
    }
}