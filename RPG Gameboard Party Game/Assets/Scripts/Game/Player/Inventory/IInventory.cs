using System.Collections;

namespace Player.Inventory
{
    public interface IInventory
    {
        int Coins { get; set; }
        IEnumerator AddCoins(int coins);
        IEnumerator RemoveCoins(int coins);
    }
}
