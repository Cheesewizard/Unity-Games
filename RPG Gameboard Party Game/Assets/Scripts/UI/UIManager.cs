using System;
using System.Collections.Generic;
using States.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
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
        public Dictionary<int, TextMeshProUGUI> playerCointsDict = new Dictionary<int, TextMeshProUGUI>();
        private readonly string _moneyHeader = "Coins ";

        public void SetupPlayerCoinsUI(int totalPlayers)
        {
            // Set player id - Needs changing later
            for (var i = 0; i < totalPlayers; i++)
            {
                // map a ui to each player, already setup in the editor
                playerCointsDict.Add(i, playerCoinsUI[i]);
            }
        }
        
        
        public void UpdateMovement(int stepsLeft)
        {
            if (stepsLeft <= 0)
            {
                playerSteps.text = null;
                return;
            }

            playerSteps.text = stepsLeft.ToString();
        }
        
        public void UpdateMoney(int playerId, int money)
        {
            if (money <= 0)
            {
                playerCointsDict[playerId].text = $"P{playerId + 1}: {_moneyHeader}{money}";
                return;
            }

            playerCointsDict[playerId].text = $"P{playerId + 1}: {_moneyHeader}{money}";
        }
    }
}
