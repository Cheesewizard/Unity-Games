using System.Collections;
using States.GameBoard;
using UnityEngine;

namespace States.Player
{
    public class NextPlayer : GameStates
    {
        public NextPlayer(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }

        public override IEnumerator Enter()
        {
            Debug.Log("Entered Next Turn");
            Debug.Log("Press Space To Continue To Next Player");
            gameSystem.turnManager.GetNextPlayer();
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            // Press space to continue
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameSystem.StartCoroutine(TransitionToState(1));
            }
        }

        private IEnumerator TransitionToState(int timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            gameSystem.SetState(new Player(gameSystem));
        }
    }
}