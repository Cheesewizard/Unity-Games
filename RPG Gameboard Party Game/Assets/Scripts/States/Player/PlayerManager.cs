using System.Collections.Generic;
using States.Player;
using UnityEngine;

public class PlayerManager
{
    public Dictionary<int, int> PlayerPosition = new Dictionary<int, int>();

    public PlayerManager(IEnumerable<GameObject> players)
    {
        foreach (var player in players)
        {
            var playerid = player.GetComponent<PlayerId>();
            AddPlayers(playerid.Id);
        }
    }

    private void AddPlayers(int playerId)
    {
        if (PlayerPosition.ContainsKey(playerId))
        {
            return;
        }

        // Everyone starts at route position 0
        PlayerPosition.Add(playerId, 0);
    }

    public void RemovePlayer(int playerId)
    {
        if (PlayerPosition.ContainsKey(playerId))
        {
            PlayerPosition.Remove(playerId);
        }
    }

    public void UpdatePlayerPosition(int playerId, int playerposition)
    {
        if (PlayerPosition.ContainsKey(playerId))
        {
            PlayerPosition[playerId] = playerposition;
        }
    }

    public int GetPlayerPosition(int playerId)
    {
        return !PlayerPosition.ContainsKey(playerId) ? 0 : PlayerPosition[playerId];
    }
}