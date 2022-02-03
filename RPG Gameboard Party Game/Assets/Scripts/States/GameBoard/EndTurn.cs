using System.Collections;
using UnityEngine;

namespace States.GameBoard
{
    public class EndTurn : GameStates
    {
        public EndTurn(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered End Turn");
            gameSystem.turnManager.GetNextPlayer();
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new StartTurn(gameSystem)));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }
        
    }
}