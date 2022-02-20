using System;
using System.Collections;
using Mirror;
using TMPro;
using UnityEngine;

namespace Manager.UI
{
    public class PlayerUIManager : NetworkBehaviour
    {
        public static PlayerUIManager Instance { get; private set; }

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


        [Header("UI Elements")] public Canvas playerStartCanvas;
        public TextMeshProUGUI playerStartText;

        public TextMeshProUGUI[] playerMoney;


        // Start
        private void Start()
        {
            CmdTogglePlayerStartCanvas(false);
        }


        // Player Start Panel
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

        // Player Name
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


        // Player Money
        [Command(requiresAuthority = false)]
        public void CmdUpdatePlayerMoney(int playerId, int amount)
        {
            RcpUpdatePlayerMoney(playerId, amount);
        }

        [ClientRpc]
        private void RcpUpdatePlayerMoney(int playerId, int amount)
        {
            //StartCoroutine(SlowlyIncrementMoney(playerId, amount));
            playerMoney[playerId].text = amount.ToString();
        }

        private IEnumerator SlowlyIncrementMoney(int playerId, int newAmount)
        {
            if (newAmount > 0)
            {
                int.TryParse(playerMoney[playerId].text, out var currentAmount);
                for (var i = 0; i < newAmount; i++)
                {
                    playerMoney[playerId].text = (currentAmount + i).ToString();
                    yield return new WaitForSeconds(0.3f);
                }
            }
            else
            {
                int.TryParse(playerMoney[playerId].text, out var currentAmount);
                for (var i = 0; i > newAmount; i--)
                {
                    playerMoney[playerId].text = (currentAmount + i).ToString();
                    yield return new WaitForSeconds(0.3f);
                }
            }
        }
    }
}