using System.Collections;
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

        private GameObject _currentPlayer;


        public PlayerMove(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Move Character");

            _currentPlayer = gameSystem.players[gameSystem.turnManager.CurrentPlayer()].gameObject;
            _routePosition = gameSystem.playerManager.GetPlayerPosition(_currentPlayer.GetComponent<PlayerId>().Id);
            
            _movePlayerPosition = _currentPlayer.GetComponent<MovePlayerToTile>();

            // Total the value of all the dice and add to amount of steps able to take
            foreach (var diceNumber in gameSystem.diceNumbers)
            {
                _steps += diceNumber;
            }

            gameSystem.StartCoroutine(Move());
            yield return null;
        }

        public override IEnumerator Exit()
        {
            // Move player smaller and move into the corner? 
            yield return null;
        }

        public override void Tick()
        {
            // remove if unused   
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
            UpdatePlayerPosition(_currentPlayer.GetComponent<PlayerId>(), _routePosition);
            
            
            //PlayerSteps?.Invoke(steps);
            //PlayerMoving?.Invoke(false);
            gameSystem.StartCoroutine(TransitionToState(1));
        }

        private void UpdatePlayerPosition(IPlayer player, int routePosition)
        {
            gameSystem.playerManager.UpdatePlayerPosition(player.Id ,routePosition);
        }

        private IEnumerator TransitionToState(int timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            gameSystem.SetState(new GameTile(gameSystem));
        }
    }
}