using System.Collections;
using GameBoard.Tiles;
using Manager.Camera;
using Manager.Player;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.GameBoard
{
    public class GameTileEffectState : GameStates
    {
        public GameTileEffectState(GameBoardSystem system) : base(system)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Game Tile");
            var tileEffect = gameSystem.currentTile.GetComponent<ITile>();
            if (tileEffect != null)
            {
                Debug.Log("Tile Activated");
                PlayerDataManager.Instance.CmdUpdatePlayerData(gameSystem.playerData.NetworkIdentity.netId,
                    tileEffect.ActivateTile(gameSystem.playerData));
            }

            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new EndTurnState(gameSystem)));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            CameraManager.Instance.RpcZoomIn();
        }
    }
}