using System;
using System.Collections;
using Manager.UI;
using UnityEngine;

namespace Player.Inventory
{
    public class Inventory : IInventory
    {
        Action<int, int> onUpdateCoins;
        private float _coinSpeed = 0.15f;

        private int _playerId;

        public Inventory()
        {
            onUpdateCoins += UIManager.Instance.UpdateMoney;
        }

        private int _coins;
        public int Coins
        {
            get { return _coins; }
            set
            {
                _coins = value;
                //onUpdateCoins?.Invoke(_playerId, Coins);
            }
        }

        public IEnumerator AddCoins(int coins)
        {
            for (var i = 0; i < coins; i++)
            {
                Coins += 1;
                yield return new WaitForSeconds(_coinSpeed);
            }
        }

        public IEnumerator RemoveCoins(int coins)
        {
            for (var i = 0; i < coins; i++)
            {
                Coins -= 1;
                yield return new WaitForSeconds(_coinSpeed);
            }
        }
    }
}