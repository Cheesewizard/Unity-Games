using System.Collections;
using System.Linq;
using States.GameBoard;
using UnityEngine;

namespace States.Player
{
    public class PlayerMove : GameStates
    {
        private int _steps;
        private bool _isMoving = false;
        private int _routePosition;
        private MovePlayerToTile _movePlayerPosition;

        public PlayerMove(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Move Character");
            Init();

            gameSystem.StartCoroutine(Move());
            yield return null;
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
            // Move player smaller and move into the corner? 
            yield return null;
        }

        public override void Tick()
        {
            gameSystem.cameraManager.ZoomOut(); 
        }

        private IEnumerator Move()
        {
            if (_isMoving)
            {
                yield break;
            }

            _isMoving = true;
            //PlayerMoving?.Invoke(true);

            while (_steps > 0)
            {
                //PlayerSteps?.Invoke(steps);

                _routePosition++;
                _routePosition %= gameSystem.currentRoute.childNodesList.Count;

                gameSystem.currentTile = gameSystem.currentRoute.childNodesList[_routePosition].gameObject;
                var nextPos = gameSystem.currentRoute.childNodesList[_routePosition].position;
                while (_movePlayerPosition.MoveToNextNode(nextPos, gameSystem.playerSpeed))
                {
                    yield return null;
                }

                yield return new WaitForSeconds(0.1f);
                _steps--;
            }

            _isMoving = false;
            UpdatePlayerPosition(gameSystem.playerData.PlayerId, _routePosition);


            //PlayerSteps?.Invoke(steps);
            //PlayerMoving?.Invoke(false);
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1, new GameTile(gameSystem)));
        }

        private void UpdatePlayerPosition(int playerId, int routePosition)
        {
            gameSystem.playerData.PositionIndex = routePosition;
            gameSystem.playerManager.UpdatePlayerData(playerId, gameSystem.playerData);
        }


    }
}