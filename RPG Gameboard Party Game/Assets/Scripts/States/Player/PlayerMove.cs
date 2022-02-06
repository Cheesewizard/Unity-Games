using System.Collections;
using System.Linq;
using Manager.Camera;
using States.GameBoard;
using States.GameBoard.StateSystem;
using UnityEngine;

namespace States.Player
{
    public class PlayerMove : GameStates
    {
        private int _steps;
        private int _routePosition;
        private MovePlayerToTile _movePlayerPosition;

        public PlayerMove(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Move Character");
            Init();

            yield return gameSystem.StartCoroutine(Move());

            // This code seems like a clunky way to manage the first turn for game start
            GameStates nextState = new GameTile(gameSystem);
            if (gameSystem.playerData.IsPlayerTurnStarted)
            {
                nextState = new StartTurn(gameSystem);
            }

            yield return gameSystem.StartCoroutine(gameSystem.TransitionToState(1, nextState));
        }

        private void Init()
        {
            _routePosition = gameSystem.playerData.PositionIndex;
            _movePlayerPosition = gameSystem.playerData.Player.GetComponent<MovePlayerToTile>();

            // Total the value of all the dice and add to amount of steps able to take
            _steps = gameSystem.diceNumbers.Sum();
        }

        public override IEnumerator Exit()
        {
            // Set first turn to false.
            if (gameSystem.playerData.IsPlayerTurnStarted)
            {
                Debug.Log("Setting First Turn to False");
                gameSystem.playerData.IsPlayerTurnStarted = false;
                gameSystem.playerDataManager.CmdUpdatePlayerData(gameSystem.playerData.NetworkIdentity.netId, gameSystem.playerData);
            }

            // Reset the dice numbers for the next player
            gameSystem.diceNumbers.Clear();

            // Move player smaller and move into the corner? 
            yield return null;
        }

        public override void Tick()
        {
            if (gameSystem.playerData.IsPlayerTurnStarted)
            {
                return;
            }

            CameraManager.Instance.RpcZoomOut();
        }

        private IEnumerator Move()
        {
            //PlayerMoving?.Invoke(true);

            while (_steps > 0)
            {
                //PlayerSteps?.Invoke(steps);
                _routePosition %= gameSystem.currentRoute.childNodesList.Count;

                gameSystem.currentTile = gameSystem.currentRoute.childNodesList[_routePosition].gameObject;
                var nextPos = gameSystem.currentRoute.childNodesList[_routePosition].position;
                while (_movePlayerPosition.MoveToNextNode(nextPos, gameSystem.playerData.PlayerSpeed))
                {
                    yield return null;
                }

                yield return new WaitForSeconds(0.1f);
                _routePosition++;
                _steps--;
            }

            UpdatePlayerPosition(gameSystem.playerData.NetworkIdentity.netId, _routePosition);

            //PlayerSteps?.Invoke(steps);
            //PlayerMoving?.Invoke(false);
        }

        private void UpdatePlayerPosition(uint playerId, int routePosition)
        {
            gameSystem.playerData.PositionIndex = routePosition;
            gameSystem.playerDataManager.CmdUpdatePlayerData(playerId, gameSystem.playerData);
        }
    }
}