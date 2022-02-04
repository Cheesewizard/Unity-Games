namespace States.Player.Inventory
{
    public interface IInventory
    {
        int Coins { get; set; }
        void AddCoins(int coins);
        void RemoveCoins(int coins);
    }
}
