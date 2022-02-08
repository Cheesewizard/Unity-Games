using System.Collections;
using Helpers;
using Manager.Player;
using Mirror;
using Player;
using UnityEngine;
using States.GameBoard;
using States.GameBoard.StateSystem;

namespace States.Menus
{
    public class CharacterSetupState : GameStates
    {
        public CharacterSetupState(GameBoardSystem system) : base(system)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Character Select Screen");
            Debug.Log("Press E To Accept");
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            // if (gameSystem.playerManager.GetTotalPlayers() < 2)
            // {
            //     Debug.Log("Not Enough Players");
            //     return;
            // }

            CheckInput();
        }

        [Client]
        private void CheckInput()
        {
            CmdStartButton();
            CheckGameConfigIsSet();
        }

        [Command]
        private void CmdStartButton()
        {
            if (!Input.GetKeyDown(KeyCode.E))
            {
                return;
            }

            if (NetworkHelpers.PlayerHasAuthority(PlayerEnum.Player1))
            {
                if (!gameSystem.startGame)
                {
                    gameSystem.SetStartingValues();
                }
            }
        }

        private void CheckGameConfigIsSet()
        {
            if (!Input.GetKeyDown(KeyCode.E))
            {
                return;
            }

            if (gameSystem.startGame && gameSystem.IsPlayerTurn())
            {
                CmdConfirmSetup();
            }
        }


        [Command]
        private void CmdConfirmSetup()
        {
            Debug.Log("Starting Game");
            GoToGameSetupState();
        }

        [ClientRpc]
        private void GoToGameSetupState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new GameSetupState(gameSystem)));
        }
    }
}