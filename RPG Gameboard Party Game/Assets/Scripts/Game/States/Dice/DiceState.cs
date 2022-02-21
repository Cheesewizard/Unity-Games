using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using Manager.Dice;
using UnityEngine;

namespace Game.States.Dice
{
    public class DiceState : GameStates
    {
        public DiceState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            DiceManager.Instance.DiceSetup(2);
            yield return null;
        }

        public override IEnumerator Exit()
        {
            DiceManager.Instance.CmdToggleDiceEnabledInScene(false);
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            CmdExitDiceButton();
            HitDiceButton();
        }

        private void CmdExitDiceButton()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;

            gameSystem.message.Log("Exit Dice Option");
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerState(gameSystem)));
        }

        private void HitDiceButton()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            DiceManager.Instance.ActivateDice();
            GoToPlayerMoveState();
        }

        private void GoToPlayerMoveState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMoveState(gameSystem)));
        }
    }
}