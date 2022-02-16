using System.Collections;

namespace Game.States.GameBoard.StateSystem
{
    public abstract class GameStates
    {
        protected GameBoardSystem gameSystem;

        protected GameStates(GameBoardSystem system)
        {
            gameSystem = system;
        }

        public virtual IEnumerator Enter()
        {
            yield break;
        }

        public virtual IEnumerator Exit()
        {
            yield break;
        }

        public virtual void Tick()
        {

        }
        
    }
}