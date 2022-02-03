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
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new Dice.Dice(gameSystem)));
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Press Button For Items");
                //gameSystem.SetState(new Dice(gameSystem));
            }  
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Press Button For Look At Game Board Camera");
                gameSystem.StartCoroutine(gameSystem.TransitionToState(0.1f, new LookAtGameBoard(gameSystem)));
            }

        }
    }
}