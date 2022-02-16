using System.Collections;
using Game.States.GameBoard.StateSystem;
using Manager.Turns;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerEndTurnState : GameStates
    {
        public PlayerEndTurnState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered End Turn");
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerWaitingState(gameSystem)));
            IncreaseTurnOrder();
            yield return null;
        }

        private void IncreaseTurnOrder()
        {
            TurnManager.Instance.CmdIncrementTurnOrder();
        }
    }
}