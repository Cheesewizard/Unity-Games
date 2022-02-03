using System.Collections;
using States.GameBoard;
using UnityEngine;

namespace States.Player
{
    public class Player : GameStates
    {
        public Player(GameBoardSystem gameSystem) : base(gameSystem)
        {
        }
        
        public override IEnumerator Enter()
        { 
            Debug.Log("Entered Player Start");
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }

        public override void Tick()
        {
            // Press space to continue
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Press Button For Dice");
                gameSystem.SetState(new Dice.Dice(gameSystem));
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Press Button For Items");
                //gameSystem.SetState(new Dice(gameSystem));
            }
            
        }

        private IEnumerator TransitionToState(int timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            gameSystem.SetState(new PlayerMove(gameSystem));
        }
    }
}