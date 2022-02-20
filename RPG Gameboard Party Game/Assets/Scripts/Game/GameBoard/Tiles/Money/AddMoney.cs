using Game.GameBoard.Items;
using Manager.Money;
using UnityEngine;

namespace Game.GameBoard.Tiles.Money
{
    public class AddMoney : MonoBehaviour, ITile
    {
        public void ActivateTile(int playerId)
        {
            // Add money
            Debug.Log("Add coins");

            var item = gameObject.GetComponent<Item>();
            if (item != null)
            {
                MoneyManager.Instance.CmdAddMoney(playerId, item.item.id);
            }
        }
    }
}