using System.Collections;
using Game.States.GameBoard;
using Game.States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerMoveOntoBoardState : PlayerMoveState
    {
        public PlayerMoveOntoBoardState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }
        
        public override IEnumerator Enter()
        {      
            // Set camera to this persons turn on start
            gameSystem.playerCamera.CmdEnablePlayerCamera();
            
            Debug.Log($"Player {gameSystem.playerId} Entered Move Character Onto The Board State");
            Init();

            yield return new WaitForSeconds(1);
            yield return gameSystem.StartCoroutine(base.Move());
            MoveToCharacterNowOnBoardState();
        }

        public override void Tick()
        {
            // Override to stop the camera zoom that happens on the base class. 
        }

        private void MoveToCharacterNowOnBoardState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new CharacterNowOnTheBoardState(gameSystem)));
        }
    }
}