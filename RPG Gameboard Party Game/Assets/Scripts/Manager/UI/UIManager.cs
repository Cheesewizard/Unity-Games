using System.Collections.Generic;
using Mirror;
using Player;
using TMPro;

namespace Manager.UI
{
    public class UIManager : NetworkBehaviour
    {
        public static UIManager Instance { get; private set; }
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

      


        public TextMeshPro playerSteps;
        
   
        public TextMeshProUGUI[] playerCoinsUI;
        public Dictionary<uint, TextMeshProUGUI> playerCointsDict = new Dictionary<uint, TextMeshProUGUI>();
        private readonly string _moneyHeader = "Coins ";

        [Command]
        public void SetupPlayerCoinsUI(List<PlayerData> playerDatas)
        {
            // Set player id - Needs changing later
            for (var i = 0; i < playerDatas.Count; i++)
            {
                // map a ui to each player, already setup in the editor
                playerCointsDict.Add(playerDatas[i].NetworkIdentity.netId, playerCoinsUI[i]);
            }
        }
        
        [ClientRpc]
        public void UpdateMovement(int stepsLeft)
        {
            if (stepsLeft <= 0)
            {
                playerSteps.text = null;
                return;
            }

            playerSteps.text = stepsLeft.ToString();
        }
        
        [ClientRpc]
        public void UpdateMoney(uint playerId, int money)
        {
            // if (money <= 0)
            // {
            //     playerCointsDict[playerId].text = $"P{playerId + 1}: {_moneyHeader}{money}";
            //     return;
            // }
            //
            // playerCointsDict[playerId].text = $"P{playerId + 1}: {_moneyHeader}{money}";
        }
    }
}
