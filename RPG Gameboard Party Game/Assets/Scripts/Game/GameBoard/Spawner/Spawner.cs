using Mirror;
using Network.Lobby;
using UnityEngine;

namespace Game.GameBoard.Spawner
{
    public class Spawner
    {
        internal static void InitialSpawn()
        {
            if (!NetworkServer.active) return;
            SpawnReward();
        }

        private static void SpawnReward()
        {
            if (!NetworkServer.active) return;

            var gameBoard = Object.Instantiate(((NetworkRoomManagerExtended) NetworkManager.singleton).gameBoard);
            NetworkServer.Spawn(Object.Instantiate(gameBoard, gameBoard.transform.position, Quaternion.identity));
        }
    }
}