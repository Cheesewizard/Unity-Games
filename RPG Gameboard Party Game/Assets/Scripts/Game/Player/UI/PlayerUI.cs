using Mirror;
using TMPro;
using UnityEngine;

namespace Game.Player.UI
{
    public class PlayerUI : NetworkBehaviour
    {
        public static PlayerUI Instance { get; private set; }

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


        public Canvas playerStartCanvas;
        public TextMeshProUGUI playerStartText;
        

        // Start
        private void Start()
        {
            CmdTogglePlayerStartCanvas(false);
        }
        
        
        [Command(requiresAuthority = false)]
        public void CmdTogglePlayerStartCanvas(bool isVisible)
        {
            RpcTogglePlayerStartCanvas(isVisible);
        }

        [ClientRpc]
        private void RpcTogglePlayerStartCanvas(bool isVisible)
        {
            playerStartCanvas.enabled = isVisible;
        }

        [Command(requiresAuthority = false)]
        public void CmdSetPlayerStartName(int playerId)
        {
            RcpSetPlayerStartName(playerId);
        }

        [ClientRpc]
        private void RcpSetPlayerStartName(int playerId)
        {
            playerStartText.text = $"Player {playerId}\n Press Space To Start";
        }
    }
}