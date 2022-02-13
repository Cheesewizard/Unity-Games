using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using Manager.Player;
using Manager.Turns;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.GameBoard
{
    public class EndTurnState : GameStates
    {
        public EndTurnState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered End Turn");
            IncreaseTurnOrder();
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.5f, new PlayerWaitingState(gameSystem)));
            yield return null;
        }

        private void IncreaseTurnOrder()
        {
            TurnManager.Instance.CmdIncrementTurnOrder();
        }
    }
}