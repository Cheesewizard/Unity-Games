using System.Collections;
using Game.States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerStartTurnState : GameStates
    {
        public PlayerStartTurnState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered Begin Turn");
            Debug.Log("Press Space To Continue");
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            CmdStartTurnButton();
        }
        
        private void CmdStartTurnButton()
        {
            // Press space to continue
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.5f, new PlayerState(gameSystem)));
            }
        }
    }
}