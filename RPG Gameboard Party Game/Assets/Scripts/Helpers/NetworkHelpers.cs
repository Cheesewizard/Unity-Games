using Mirror;
using UnityEngine;

namespace Helpers
{
    public static class NetworkHelpers
    {

        [Command (requiresAuthority = false)]
        public static void CmdToggleObjectVisibility(GameObject gameObject, bool isVisible)
        {
            RcpToggleObjectVisibility(gameObject, isVisible);
        }
        
        [ClientRpc]
        public static void  RcpToggleObjectVisibility(GameObject gameObject, bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}