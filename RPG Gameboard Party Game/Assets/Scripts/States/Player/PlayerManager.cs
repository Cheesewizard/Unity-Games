using System.Collections.Generic;
using System.Linq;
using States.Player;
using States.Player.Inventory;
using UI;
using UnityEngine;

public class PlayerManager
{
    public Dictionary<int, PlayerData> PlayerDataDict = new Dictionary<int, PlayerData>();

    private GameObject[] _players;
    private PlayerData _playerData;

    public PlayerManager(GameObject[] players)
    {
        _players = players;
        Init();
    }

    private void Init()
    {
        // Set player id - Needs changing later
        for (var i = 0; i < _players.Length; i++)
        {
            var playerId = _players[i].GetComponent<PlayerId>();
            playerId.Id = i;

            AddPlayer(playerId.Id, _players[i].gameObject);
        }
    }

    private void AddPlayer(int playerId, GameObject player)
    {
        if (PlayerDataDict.ContainsKey(playerId))
        {
            return;
        }

        var data = new PlayerData()
        {
            PlayerId = playerId,
            Player = player,
            PositionIndex = 0,
            Inventory = new Inventory(playerId)
        };
        
        PlayerDataDict.Add(playerId, data);
    }
    
    

    public void RemovePlayer(int playerId)
    {
        if (PlayerDataDict.ContainsKey(playerId))
        {
            PlayerDataDict.Remove(playerId);
        }
    }

    public void UpdatePlayerData(int playerId, PlayerData data)
    {
        if (PlayerDataDict.ContainsKey(playerId))
        {
            PlayerDataDict[playerId] = data;
        }
    }

    public PlayerData GetPlayerData(int playerId)
    {
        return !PlayerDataDict.ContainsKey(playerId) ? new PlayerData() : PlayerDataDict[playerId];
    }

    public PlayerData GetPlayerDataFromPlayerIndex(int index)
    {
        for (var i = 0; i < _players.Length; i++)
        {
            if (i == index)
            {                
                return PlayerDataDict.ElementAt(index).Value;
            }
        }

        return new PlayerData();
    }
}