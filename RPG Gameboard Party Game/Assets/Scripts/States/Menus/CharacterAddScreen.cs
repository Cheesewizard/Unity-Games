using System.Collections;
using GameBoard.Tiles;
using UnityEngine;
using States.GameBoard;
using States.GameBoard.StateSystem;

namespace States.Menus
{
    public class CharacterAddScreen : GameStates
    {
        public CharacterAddScreen(GameBoardSystem system) : base(system)
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
            if (!Input.GetKeyDown(KeyCode.E))
            {
                return;
            }

            // if (gameSystem.playerManager.GetTotalPlayers() < 2)
            // {
            //     Debug.Log("Not Enough Players");
            //     return;
            // }

            gameSystem.ConfirmSetup();
            Debug.Log("Starting Game");
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new StartGame(gameSystem)));
        }
    }
}