using System.Collections;
using Game.States.GameBoard;
using Game.States.GameBoard.StateSystem;
using Player;
using UnityEngine;

namespace Game.States.Menus
{
    public class CharacterSetupState : GameStates
    {
        public CharacterSetupState(GameBoardSystem system) : base(system)
        {
        }

        public override IEnumerator Enter()
        {
            if (gameSystem.playerId != (int) PlayerEnum.Player1)
            {
                GoToGameSetupState();
            }
            
            gameSystem.message.Log($" Player {gameSystem.playerId} Entered Character Select Screen");
            gameSystem.message.Log("Press E To Accept");
            yield return null;
        }

        public override void Tick()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            CmdSetConfig();
        }

        private void CmdSetConfig()
        {
            if (!Input.GetKeyUp(KeyCode.E))
            {
                return;
            }

            if (!gameSystem.startGame && gameSystem.playerId == (int) PlayerEnum.Player1)
            {
                gameSystem.SetStartingValues();
                CheckGameConfigIsSet();
            }
        }
        
        private void CheckGameConfigIsSet()
        {
            if (!gameSystem.startGame) return;

            gameSystem.message.Log("Game Config Set");
            GoToGameSetupState();
        }

        private void GoToGameSetupState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new GameSetupState(gameSystem)));
        }
    }
}