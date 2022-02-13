using System.Collections;
using System.Linq;
using Game.States.GameBoard;
using Game.States.GameBoard.StateSystem;
using Helpers;
using Manager.Camera;
using Manager.Player;
using Mirror;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace Game.States.Player
{
    public class PlayerMoveState : GameStates
    {
        private int _steps;
        private int _routePosition;
        private MoveToTarget _movePosition;

        public PlayerMoveState(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log($"Player {gameSystem.playerId} Entered Move Character");
            Init();

            yield return gameSystem.StartCoroutine(Move());
            yield return gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new PlayerCheckTileState(gameSystem)));
        }

        protected void Init()
        {
            var target = PlayerDataManager.Instance.currentPlayerData.Identity.gameObject;
            _routePosition = PlayerDataManager.Instance.currentPlayerData.boardLocationIndex;
            _movePosition = target.GetComponent<MoveToTarget>();

            // Total the value of all the dice and add to amount of steps able to take
            _steps = gameSystem.diceNumbers.Sum();
        }

        public override IEnumerator Exit()
        {
            // Reset the dice numbers for the next player
            gameSystem.diceNumbers.Clear();

            // Move player smaller and move into the corner? 
            yield return null;
        }

        public override void Tick()
        {
            CameraManager.Instance.CmdZoomOut();
        }

        protected IEnumerator Move()
        {
            //PlayerMoving?.Invoke(true);

            while (_steps > 0)
            {
                //PlayerSteps?.Invoke(steps);
                _routePosition %= gameSystem.gameBoard.childNodesList.Count;

                gameSystem.currentTile = gameSystem.gameBoard.childNodesList[_routePosition].gameObject;
                var nextPos = gameSystem.gameBoard.childNodesList[_routePosition].position;
                while (_movePosition.MoveToNextNode(nextPos, PlayerDataManager.Instance.currentPlayerData.PlayerSpeed))
                {
                    yield return null;
                }

                yield return new WaitForSeconds(0.1f);
                _routePosition++;
                _steps--;
            }

            UpdatePlayerPosition();

            //PlayerSteps?.Invoke(steps);
            //PlayerMoving?.Invoke(false);
        }

        private void UpdatePlayerPosition()
        {
            var data = PlayerDataManager.Instance.currentPlayerData;
            data.boardLocationIndex = _routePosition;
            PlayerDataManager.Instance.CmdUpdatePlayerData(PlayerDataManager.Instance.currentPlayerData.playerId, data);
        }
    }
}