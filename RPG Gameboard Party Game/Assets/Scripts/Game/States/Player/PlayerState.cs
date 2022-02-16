using System.Collections;
using Game.States.GameBoard;
using Game.States.GameBoard.StateSystem;
using Mirror;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerState : GameStates
    {
        public PlayerState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered Start Turn");
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
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
        
        private void GoToDiceState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Dice.DiceState(gameSystem)));
        }
        
        private void GoToGameBoardCameraState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new GameBoardCameraState(gameSystem)));
        }
        
        private void GoToInventoryState()
        {
        }
    }
}