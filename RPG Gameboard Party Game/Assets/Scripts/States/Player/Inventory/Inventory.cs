using System;
using UI;

namespace States.Player.Inventory
{
    public class Inventory : IInventory
    { 
        Action<int, int> onUpdateCoins;

        private int _playerId;
        public Inventory(int playerId)
        {
            _playerId = playerId;
            onUpdateCoins += UIManager.Instance.UpdateMoney;
        }
        
        private int _coins;
        public int Coins
        {
            get { return _coins; }
            set
            {
                _coins = value;
                onUpdateCoins?.Invoke(_playerId, Coins);
            }
        }

        public void AddCoins(int coins)
        {
            Coins += coins;
        }
        public void RemoveCoins(int coins)
        {
            Coins -= coins;
        }
    }
}