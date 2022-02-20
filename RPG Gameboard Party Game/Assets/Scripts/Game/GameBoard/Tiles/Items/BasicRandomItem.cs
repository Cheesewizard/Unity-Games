using Manager.Inventory;
using ScriptableObject.Inventory;
using UnityEngine;

namespace Game.GameBoard.Tiles.Items
{
    public class BasicRandomItem : MonoBehaviour, ITile
    {
        public InventoryObject container;

        public void ActivateTile(int playerId)
        {
            Debug.Log("Add random basic item");

            if (container.inventory.Count > 0)
            {
                var index = Random.Range(0, container.inventory.Count);
                var inv = container.inventory[index];

                InventoryManager.Instance.CmdAddToInventory(playerId, inv.item.id, inv.amount);
            }
        }
    }
}