using Mirror;
using UnityEngine;

namespace Manager.Turns
{
    public class TurnManager : NetworkBehaviour
    {
        [SyncVar] public int _totalPlayers;
        [SyncVar] public int _maxTurns;
        [SyncVar] public int _currentTurn;
        [SyncVar] public int currentPlayerTurnOrder;

        public static TurnManager Instance { get; private set; }

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

        public void SetTotalPlayers(int totalPlayers)
        {
            _totalPlayers = totalPlayers;
        }

        public void SetTotalTurns(int maxTurns)
        {
            _maxTurns = maxTurns;
        }


        [Command(requiresAuthority = false)]
        public void CmdIncrementTurnOrder()
        {
            IncrementPlayerIndex();
            IncrementTotalTurns();
            IncrementTurnOrder();
        }
        
        private void IncrementPlayerIndex()
        {
            currentPlayerTurnOrder += 1;
        }
        
        private void IncrementTotalTurns()
        {
            // if total turns == max turns, exit game?
            if (_currentTurn == _maxTurns)
            {
                Debug.Log("Last Turn");
            }

            _currentTurn++;
        }
        
        private void IncrementTurnOrder()
        {
            if (_totalPlayers == 1)
            {
                return;
            }

            currentPlayerTurnOrder %= _totalPlayers;
        }
    }
}