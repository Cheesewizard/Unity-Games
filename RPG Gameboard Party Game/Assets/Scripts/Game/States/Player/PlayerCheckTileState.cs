using System.Collections;
using Game.GameBoard.Tiles;
using Game.States.GameBoard.StateSystem;
using Manager.Player;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerCheckTileState : GameStates
    {
        public PlayerCheckTileState(GameBoardSystem system) : base(system)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered Game Tile");
            var tileEffect = gameSystem.currentTile.GetComponent<ITile>();
            if (tileEffect != null)
            {
                Debug.Log("Tile Activated");
                var data = tileEffect.ActivateTile(
                    PlayerDataManager.Instance.clientPlayerDataDict[gameSystem.playerId]);
                PlayerDataManager.Instance.CmdUpdatePlayerData(gameSystem.playerId, data);
                yield return new WaitForSeconds(2);
            }

            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new PlayerEndTurnState(gameSystem)));
            yield return null;
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