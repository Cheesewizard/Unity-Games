using System.Collections;
using Helpers;
using Manager.Dice;
using Mirror;
using States.GameBoard;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.Player
{
    public class PlayerState : GameStates
    {
        public PlayerState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Player Start");
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            if (gameSystem.IsPlayerTurn())
            {
                CheckInput();
            }
        }

        [Client]
        private void CheckInput()
        {
            CmdDiceButton();
            CmdItemButton();
            CmdCameraButton();
        }

        [Command]
        private void CmdDiceButton()
        {
            // Press space to continue
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Press Button For Dice");
                GoToDiceState();
            }
        }

        [Command]
        private void CmdItemButton()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Press Button For Items");
                GoToInventoryState();
            }
        }

        [Command]
        private void CmdCameraButton()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Press Button For Look At Game Board Camera");
                GoToGameBoardCameraState();
            }
        }

        [ClientRpc]
        private void GoToDiceState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Dice.DiceState(gameSystem)));
        }

        [ClientRpc]
        private void GoToGameBoardCameraState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new GameBoardCameraState(gameSystem)));
        }

        [ClientRpc]
        private void GoToInventoryState()
        {
        }
    }
}