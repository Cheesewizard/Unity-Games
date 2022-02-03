using System.Collections;
using States.GameBoard;

namespace States.Player
{
    public class PlayerStart : GameStates
    {
        public PlayerStart(GameBoardSystem gameSystem) : base(gameSystem)
        {
            
        }
        
        public override IEnumerator Enter()
        {
            gameSystem.SetState(new PlayerTurnState(gameSystem));
            yield return null;
        }

        public override IEnumerator Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}