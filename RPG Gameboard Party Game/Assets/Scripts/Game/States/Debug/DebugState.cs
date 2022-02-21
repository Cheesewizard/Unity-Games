using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using Manager.Movement;
using UnityEngine;

namespace Game.States.Debug
{
    public class DebugState : GameStates
    {
        public DebugState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            gameSystem.debugMenu.ToggleDebugMenu(true);
            gameSystem.debugMenu.diceNumber += SetDiceAmount;
            yield return null;
        }


        public override IEnumerator Exit()
        {
            gameSystem.debugMenu.ToggleDebugMenu(false);
            gameSystem.debugMenu.diceNumber -= SetDiceAmount;
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            ExitDebugMenu();
        }

        private void ExitDebugMenu()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                gameSystem.debugMenu.ToggleDebugMenu(false);
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, gameSystem.previousState));
            }
        }

        private void SetDiceAmount(int diceNumber)
        {
            MovementManager.Instance.CmdAddMovementAmount(diceNumber);
            GoToPlayerMoveState();
        }

        private void GoToPlayerMoveState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new PlayerMoveState(gameSystem)));
        }
    }
}