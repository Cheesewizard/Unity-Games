using System.Collections;
using Game.GameBoard.Tiles;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using UnityEngine;

namespace Game.States.GameBoard
{
    public class TileCheckState : GameStates
    {
        public TileCheckState(GameBoardSystem system) : base(system)
        {
        }

        public override IEnumerator Enter()
        {
            gameSystem.message.Log($"Player {gameSystem.playerId} Entered Game Tile");
            yield return CheckTile();

            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new PlayerEndTurnState(gameSystem)));
            yield return null;
        }

        private IEnumerator CheckTile()
        {
            var tile = gameSystem.currentTile.GetComponent<ITile>();
            if (tile == null) yield break;

            gameSystem.message.Log("Tile Activated");
            yield return DoTileEffect(tile);
        }

        private IEnumerator DoTileEffect(ITile tile)
        {
            tile.ActivateTile(gameSystem.playerId);
            yield return new WaitForSeconds(2);
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            gameSystem.playerCamera.CmdZoomIn();
        }
    }
}