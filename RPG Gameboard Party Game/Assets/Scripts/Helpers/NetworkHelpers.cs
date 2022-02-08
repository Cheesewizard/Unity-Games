using Manager.Player;
using Mirror;
using Player;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Helpers
{
    public static class NetworkHelpers
    {
        public static bool PlayerHasAuthority(PlayerEnum player)
        {
            return PlayerDataManager.Instance.CmdGetPlayerDataFromIndex((int) player).NetworkIdentity
                .hasAuthority;
        }

        [ClientRpc]
        public static void RpcToggleObjectVisibility(GameObject gameObject, bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}