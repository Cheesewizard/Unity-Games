using System.Collections;
using Game.States.GameBoard.StateSystem;

namespace States.GameBoard.StateSystem
{
    public abstract class GameStates
    {
        protected GameBoardSystem gameSystem;

        public GameStates(GameBoardSystem system)
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