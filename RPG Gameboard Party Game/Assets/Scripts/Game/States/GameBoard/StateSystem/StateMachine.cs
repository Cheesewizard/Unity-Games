using Mirror;

namespace Game.States.GameBoard.StateSystem
{
    public class StateMachine : NetworkBehaviour
    {
        public GameStates previousState;
        protected GameStates currentState;

        protected void SetState(GameStates state)
        {
            previousState = currentState;
            StartCoroutine(currentState.Exit());
            currentState = state;
            StartCoroutine(currentState.Enter());
        }
    }
}