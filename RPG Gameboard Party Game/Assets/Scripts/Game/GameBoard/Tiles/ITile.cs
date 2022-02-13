using Game.Player;

namespace Game.GameBoard.Tiles
{
    public interface ITile
    {
        PlayerData ActivateTile(PlayerData playerData);
    }
}
