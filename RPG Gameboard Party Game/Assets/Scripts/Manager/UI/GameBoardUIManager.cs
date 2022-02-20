using Game.Player;
using Mirror;
using UnityEngine;

namespace Manager.UI
{
    public class GameBoardUIManager : NetworkBehaviour
    {
        public static GameBoardUIManager Instance { get; private set; }

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

        [Header("Players UI Panels")] public GameObject[] playerUIPanels;


        // Fires when a player is added at startup from player data manager
        [Command(requiresAuthority = false)]
        public void CmdTogglePlayerUIPanels(PlayerData playerData)
        {
            RcpTogglePlayerUIPanels(playerData.playerId);
        }
        
        [ClientRpc]
        private void RcpTogglePlayerUIPanels(int index)
        {
            playerUIPanels[index].SetActive(true);
        }
    }
}