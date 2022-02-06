using Mirror;
using UnityEngine;

namespace Manager.Turns
{
    public class TurnManager
    {
        [SyncVar] private bool _firstTurn;
        [SyncVar] private int _totalPlayers;
        [SyncVar] private int _maxTurns;
        [SyncVar] private int _currenturn;
        [SyncVar] private int _currentPlayerIndex = 0;

        public TurnManager(int totalPlayers, int maxTurns)
        {
            _totalPlayers = totalPlayers;
            _maxTurns = maxTurns;
        }

        [Server]
        public int IncrementTurn()
        {
            IncrementPlayerIndex();
            IncrementTotalTurns();
            return _currentPlayerIndex %= _totalPlayers;
        }

        [Server]
        public int GetCurrentPlayerIndex()
        {
            return _currentPlayerIndex;
        }

        [Server]
        private void IncrementTotalTurns()
        {
            // if total turns == max turns, exit game?
            if (_currenturn == _maxTurns)
            {
                Debug.Log("last Turn");
            }

            _currenturn++;
        }

        [Server]
        private void IncrementPlayerIndex()
        {
            _currentPlayerIndex++;
        }
    }
}