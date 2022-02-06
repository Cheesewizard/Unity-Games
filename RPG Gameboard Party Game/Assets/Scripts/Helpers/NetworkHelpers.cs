using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Helpers
{
    public static class NetworkHelpers
    {
        [ClientRpc]
        public static void RpcToggleObjectVisibility(GameObject gameObject, bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
        
        public static void SpawnObjectOnServerForEveryPlayer(GameObject prefab)
        {
            foreach (var connectionToClient in GameBoardSystem.Instance.playerDataManager.CmdGetAllPlayerData())
            {
                NetworkServer.Spawn(prefab, connectionToClient.NetworkIdentity.connectionToClient);
            }
        }
    }
}