using Mirror;

namespace States.GameBoard.StateSystem
{
    public class StateMachine : NetworkBehaviour
    {
        protected GameStates currentState;

        protected void SetState(GameStates state)
        {
            StartCoroutine(currentState.Exit());
            currentState = state;
            StartCoroutine(currentState.Enter());
        }
    }
}