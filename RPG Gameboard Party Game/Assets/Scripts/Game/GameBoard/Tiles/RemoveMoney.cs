using Game.Player;
using UnityEngine;

namespace Game.GameBoard.Tiles
{
    public class RemoveMoney : MonoBehaviour, ITile
    {
        public PlayerData ActivateTile(PlayerData playerData)
        {
            Debug.Log("Remove 3 coins");
            //StartCoroutine(playerData.Inventory.RemoveCoins(3));
            return playerData;
        }
    }
}