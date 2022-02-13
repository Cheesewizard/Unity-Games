using System.Collections;
using Camera;
using Game.States.GameBoard;
using Game.States.GameBoard.StateSystem;
using Manager.Camera;
using Manager.Player;
using Player;
using States.GameBoard.StateSystem;
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
            if (PlayerDataManager.Instance.currentPlayerData.playerId != (int) PlayerEnum.Player1)
            {
                GoToGameSetupState();
            }
            
            Debug.Log($" Player {gameSystem.playerId} Entered Character Select Screen");
            Debug.Log("Press E To Accept");
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

            if (!gameSystem.startGame &&
                PlayerDataManager.Instance.currentPlayerData.playerId == (int) PlayerEnum.Player1)
            {
                gameSystem.SetStartingValues();
                CheckGameConfigIsSet();
            }
        }
        
        private void CheckGameConfigIsSet()
        {
            if (!gameSystem.startGame) return;

            Debug.Log("Game Config Set");
            GoToGameSetupState();
        }

        private void GoToGameSetupState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new GameSetupState(gameSystem)));
        }
    }
}