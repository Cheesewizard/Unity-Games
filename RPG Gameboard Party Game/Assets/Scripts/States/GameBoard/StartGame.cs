using System.Collections;
using Manager.Dice;
using States.GameBoard.StateSystem;
using States.Player;
using UnityEngine;


namespace States.GameBoard
{
    public class StartGame : GameStates
    {
        private MovePlayerToTile _movePlayerPosition;
        public StartGame(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Start Game");
            SetMovementAmount(1);
            gameSystem.StartCoroutine(gameSystem.TransitionToState(1f, new PlayerMove(gameSystem)));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }
        
        private void SetMovementAmount(int steps)
        {
            for (var i = 0; i < DiceManager.Instance.diceInScene.Count; i++)
            {
                // Only sets 1 dice to be the steps to move. 
                gameSystem.diceNumbers.Add(steps);
                break;
            }
        }
    }
}