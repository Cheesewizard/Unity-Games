using System;
using System.Collections.Generic;
using States.Player.Inventory;
using UnityEngine;

[Obsolete]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public Dictionary<int, IInventory> _inventory = new Dictionary<int, IInventory>();

    public void AddInventory(int player, IInventory inventory)
    {
        this._inventory.Add(player, inventory);
    }

    public void RemoveInventory(int player)
    {
        this._inventory.Remove(player);
    }

    public IInventory GetPlayerInventory(int player)
    {
        return this._inventory[player];
    }

    public void UpdatePlayerInventory(int player, IInventory inventory)
    {
        this._inventory[player] = inventory;
    }

}
