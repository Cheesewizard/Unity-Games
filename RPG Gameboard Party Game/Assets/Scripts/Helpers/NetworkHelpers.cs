using Manager.Player;
using Mirror;
using Player;
using UnityEngine;

namespace Helpers
{
    public static class NetworkHelpers
    {
        public static bool PlayerHasAuthority(PlayerEnum player)
        {
            //return PlayerDataManager.Instance.currentPlayerData.NetworkIdentity.hasAuthority;
            return true;
        }

        [ClientRpc]
        public static void RpcToggleObjectVisibility(GameObject gameObject, bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}