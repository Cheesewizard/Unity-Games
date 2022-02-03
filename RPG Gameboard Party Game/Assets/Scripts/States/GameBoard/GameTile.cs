using System.Collections;
using States;
using States.GameBoard;
using States.Player;
using UnityEngine;

namespace States.GameBoard
{
    public class GameTile : GameStates
    {
        public GameTile(GameBoardSystem system) : base(system)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Game Tile");
            var tileEffect = gameSystem.currentTile.GetComponent<ITile>();
            if (tileEffect != null)
            {
                Debug.Log("Tile Activated");
                tileEffect.ActivateTile();
            }
            
            gameSystem.StartCoroutine(TransitionToState(1));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        private IEnumerator TransitionToState(int timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            gameSystem.SetState(new NextPlayer(gameSystem));
        }
    }
}