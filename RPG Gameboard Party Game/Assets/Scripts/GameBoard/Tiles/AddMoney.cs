using Player;
using States.Player;
using UnityEngine;

namespace GameBoard.Tiles
{
    public class AddMoney : MonoBehaviour, ITile
    {
        public PlayerData ActivateTile(PlayerData playerData)
        {
            // Add money
            Debug.Log("Add 3 coins");
            StartCoroutine(playerData.Inventory.AddCoins(3));
            return playerData;
        }
    }
}