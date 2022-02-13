using System.Collections;
using Game.GameBoard.Tiles;
using Game.States.GameBoard.StateSystem;
using Manager.Camera;
using Manager.Player;
using States.GameBoard.StateSystem;
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
                var data = tileEffect.ActivateTile(PlayerDataManager.Instance.currentPlayerData);
                PlayerDataManager.Instance.CmdUpdatePlayerData(PlayerDataManager.Instance.currentPlayerData.playerId,data);
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
            CameraManager.Instance.CmdZoomIn();
        }
    }
}