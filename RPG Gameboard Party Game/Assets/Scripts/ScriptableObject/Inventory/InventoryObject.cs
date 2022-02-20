using System;
using System.Collections.Generic;
using ScriptableObject.Items;
using UnityEngine;

namespace ScriptableObject.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : UnityEngine.ScriptableObject
    {
        public List<InventorySlot> inventory = new List<InventorySlot>();

        public void AddToInventory(ItemObject item, int amount)
        {
            var hasItem = false;
            for (var i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].item != item) continue;

                inventory[i].AddAmount(amount);
                hasItem = true;
                break;
            }

            if (!hasItem)
            {
                inventory.Add(new InventorySlot(item, amount));
            }
        }

        public void RemoveFromInventory(ItemObject item, int amount)
        {
            for (var i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].item != item) continue;

                inventory[i].RemoveAmount(amount);
                break;
            }
        }
    }

    [Serializable]
    public struct InventorySlot
    {
        public ItemObject item;
        public int amount;

        public InventorySlot(ItemObject item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }

        public void RemoveAmount(int value)
        {
            amount -= value;
        }
    }
}