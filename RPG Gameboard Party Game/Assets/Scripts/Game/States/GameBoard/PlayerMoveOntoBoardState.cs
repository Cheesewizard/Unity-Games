using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using UnityEngine;

namespace Game.States.GameBoard
{
    public class PlayerMoveOntoBoardState : PlayerMoveState
    {
        public PlayerMoveOntoBoardState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Move Character Onto The Board State");
            Init();

            yield return gameSystem.StartCoroutine(base.Move());
            MoveToCharacterNowOnBoardState();
        }

        public override void Tick()
        {
            
        }

        private void MoveToCharacterNowOnBoardState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new CharacterNowOnTheBoardState(gameSystem)));
        }
    }
}