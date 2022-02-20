using Game.GameBoard.Items;
using Manager.Money;
using UnityEngine;

namespace Game.GameBoard.Tiles.Money
{
    public class RemoveMoney : MonoBehaviour, ITile
    {
        public void ActivateTile(int playerId)
        {
            // Remove money
            Debug.Log("Remove coins");

            var item = gameObject.GetComponent<Item>();
            if (item != null)
            {
                MoneyManager.Instance.CmdRemoveMoney(playerId, item.item.id);
            }
        }
    }
}