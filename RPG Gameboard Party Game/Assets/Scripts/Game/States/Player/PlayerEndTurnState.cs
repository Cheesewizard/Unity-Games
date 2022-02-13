using System.Collections;
using Game.States.GameBoard.StateSystem;
using Manager.Turns;
using States.GameBoard.StateSystem;
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