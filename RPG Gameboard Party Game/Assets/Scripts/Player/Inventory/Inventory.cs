using System;
using System.Collections;
using Manager.UI;
using UnityEngine;

namespace Player.Inventory
{
    public class Inventory : IInventory
    {
        Action<uint, int> onUpdateCoins;
        private float _coinSpeed = 0.15f;

        private uint _playerId;

        public Inventory(uint playerId)
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