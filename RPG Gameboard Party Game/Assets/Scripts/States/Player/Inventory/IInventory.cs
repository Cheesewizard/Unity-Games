public interface IInventory
{
    int Coins { get; set; }

    void AddCoins(int money);
    void RemoveCoins(int money);
}
