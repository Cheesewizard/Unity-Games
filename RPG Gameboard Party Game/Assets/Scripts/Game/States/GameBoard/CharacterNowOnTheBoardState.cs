using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using UnityEngine;

namespace Game.States.GameBoard
{
    public class CharacterNowOnTheBoardState : GameStates
    {
        public CharacterNowOnTheBoardState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Now On The Board");
            GoToPlayerWaitingState();
            yield return null;
        }

        private void GoToPlayerWaitingState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerWaitingState(gameSystem)));
        }
    }
}