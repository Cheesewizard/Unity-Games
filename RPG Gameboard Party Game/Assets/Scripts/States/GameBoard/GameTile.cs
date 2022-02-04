using System.Collections;
using GameBoard.Tiles;
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
                gameSystem.playerManager.UpdatePlayerData(gameSystem.playerData.PlayerId,
                    tileEffect.ActivateTile(gameSystem.playerData));
            }

            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new EndTurn(gameSystem)));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            gameSystem.cameraManager.ZoomIn();
        }
    }
}