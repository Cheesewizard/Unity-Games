using System.Collections;
using Game.States.GameBoard.StateSystem;
using Manager.UI;
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
            PlayerUIManager.Instance.CmdSetPlayerStartName(gameSystem.playerId);
            PlayerUIManager.Instance.CmdTogglePlayerStartCanvas(true);

            gameSystem.message.Log($"Player {gameSystem.playerId} Entered Begin Turn");
            gameSystem.message.Log("Press Space To Continue");
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
            PlayerUIManager.Instance.CmdTogglePlayerStartCanvas(false);
        }
    }
}