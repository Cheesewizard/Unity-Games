using System.Collections;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.GameBoard
{
    public class StartTurn : GameStates
    {
        public StartTurn(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Start Turn");
            Debug.Log("Press Space To Continue");
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            if (gameSystem.CheckIfHasAuthority())
            {
                CheckInput();
            }
        }

        private void CheckInput()
        {
            CmdStartTurnButton();
        }

        [Command]
        private void CmdStartTurnButton()
        {
            // Press space to continue
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.5f, new Player.Player(gameSystem)));
            }
        }
    }
}