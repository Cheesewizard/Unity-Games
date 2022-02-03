using UnityEngine;

public class Inventory : MonoBehaviour, IInventory
{
    public int Coins { get; set ; }

    public void AddCoins(int money)
    {
        this.Coins += money;
    }

    public void RemoveCoins(int money)
    {
        this.Coins -= money;
    }
}
