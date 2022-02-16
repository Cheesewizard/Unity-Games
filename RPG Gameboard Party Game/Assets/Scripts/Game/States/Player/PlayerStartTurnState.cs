using System.Collections;
using Game.Player.UI;
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
            PlayerUI.Instance.CmdSetPlayerStartName(gameSystem.playerId);
            PlayerUI.Instance.CmdTogglePlayerStartCanvas(true);

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
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerState(gameSystem)));
            PlayerUI.Instance.CmdTogglePlayerStartCanvas(false);
        }
    }
}