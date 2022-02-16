using System.Collections;
using System.Linq;
using Game.States.GameBoard.StateSystem;
using Helpers;
using Manager;
using Manager.Dice;
using Manager.Player;
using Manager.Turns;
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
            yield return gameSystem.StartCoroutine(
                gameSystem.TransitionToState(1, new PlayerCheckTileState(gameSystem)));
        }

        protected void Init()
        {
            var target = PlayerDataManager.Instance.clientPlayerDataDict[TurnManager.Instance.currentPlayerTurnOrder]
                .Identity.gameObject;
            _routePosition = PlayerDataManager.Instance
                .clientPlayerDataDict[TurnManager.Instance.currentPlayerTurnOrder].boardLocationIndex;
            _movePosition = target.GetComponent<MoveToTarget>();

            // Total the value of all the dice and add to amount of steps able to take
            _steps = MovementManager.Instance.movement.Sum();
        }

        public override IEnumerator Exit()
        {
            // Reset the dice numbers for the next player
            MovementManager.Instance.CmdClearMovement();

            DiceManager.Instance.CmdRemoveDiceFromScene();

            // Move player smaller and move into the corner? 
            yield return null;
        }

        public override void Tick()
        {
            gameSystem.playerCamera.CmdZoomOut();
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
                while (_movePosition.MoveToNextNode(nextPos,
                           PlayerDataManager.Instance.clientPlayerDataDict[TurnManager.Instance.currentPlayerTurnOrder]
                               .PlayerSpeed))
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
            var data = PlayerDataManager.Instance.clientPlayerDataDict[gameSystem.playerId];
            data.boardLocationIndex = _routePosition;
            PlayerDataManager.Instance.CmdUpdatePlayerData(gameSystem.playerId, data);
        }
    }
}