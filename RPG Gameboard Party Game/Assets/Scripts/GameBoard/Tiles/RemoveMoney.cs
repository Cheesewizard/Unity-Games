using States.Player;
using UnityEngine;

namespace GameBoard.Tiles
{
    public class RemoveMoney : MonoBehaviour, ITile
    {
        public PlayerData ActivateTile(PlayerData playerData)
        {
            Debug.Log("Remove 3 coins");
            playerData.Inventory.RemoveCoins(3);
            return playerData;
        }
    }
}