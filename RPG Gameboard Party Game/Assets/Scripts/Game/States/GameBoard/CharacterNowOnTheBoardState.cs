using System.Collections;
using Game.States.GameBoard.StateSystem;
using Game.States.Player;
using Manager.Player;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.GameBoard
{
    public class CharacterNowOnTheBoardState : GameStates
    {
        public CharacterNowOnTheBoardState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            var player = PlayerDataManager.Instance.currentPlayerData;
            Debug.Log($"Player {gameSystem.playerId} Now On The Board");
            PlayerDataManager.Instance.CmdUpdatePlayerData(player.playerId, player);
            GoToPlayerWaitingState();

            yield return null;
        }

        private void GoToPlayerWaitingState()
        {
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerWaitingState(gameSystem)));
        }
    }
}