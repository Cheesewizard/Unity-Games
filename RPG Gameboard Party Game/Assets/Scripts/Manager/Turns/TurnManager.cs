using Mirror;
using Player;
using UnityEngine;

namespace Manager.Turns
{
    public class TurnManager
    {
        [SyncVar] private int _totalPlayers;
        [SyncVar] private int _maxTurns;
        [SyncVar] private int _currentTurn;
        [SyncVar] private int _currentPlayerIndex = 0;

        private static readonly TurnManager instance = new TurnManager();
        public static TurnManager Instance => instance;

        static TurnManager()
        {
        }

        private TurnManager()
        {
        }
        
        [Command]
        public void SetTotalPlayers(int totalPlayers)
        {
            _totalPlayers = totalPlayers;
        }
        
        [Command]
        public void SetTotalTurns(int maxTurns)
        {
            _maxTurns = maxTurns;
        }
        
        
        public void CmdIncrementTurnOrder()
        {
            IncrementPlayerIndex();
            IncrementTotalTurns();
            IncrementTurnOrder();
        }
        
        [ClientRpc]
        private void IncrementPlayerIndex()
        {
            _currentPlayerIndex += 1;
        }
        
        [ClientRpc]
        private void IncrementTotalTurns()
        {
            // if total turns == max turns, exit game?
            if (_currentTurn == _maxTurns)
            {
                Debug.Log("last Turn");
            }

            _currentTurn++;
        }
        
        [ClientRpc]
        private void IncrementTurnOrder()
        {
            _currentPlayerIndex %= _totalPlayers;
        }
        
        public PlayerEnum CmdGetCurrentPlayer()
        {
            return (PlayerEnum) _currentPlayerIndex;
        }
    }
}