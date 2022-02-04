using States.Player;

namespace GameBoard.Tiles
{
    public interface ITile
    {
        PlayerData ActivateTile(PlayerData playerData);
    }
}
