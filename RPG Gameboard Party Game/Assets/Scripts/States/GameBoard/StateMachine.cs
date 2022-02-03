using States.Player;
using UnityEngine;

namespace States
{
    public class StateMachine : MonoBehaviour
    {
        protected GameStates currentState;

        public void SetState(GameStates state)
        {
            StartCoroutine(currentState.Exit());
            currentState = state;
            StartCoroutine(currentState.Enter());
        }
    }
}