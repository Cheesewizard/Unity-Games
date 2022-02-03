using System.Collections;
using System.Collections.Generic;
using States.Player;
using UnityEngine;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
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

    public List<IDice> dices = new List<IDice>();
    private List<IPlayer> playerCount = new List<IPlayer>();
    public Route currentRoute;

    // Start is called before the first frame update

    // Switch players, player camera

    // Inventory, roll dice?

    // Roll Dice, animate, get number

    // Move


    public void PlayerSetup(IPlayer player)
    {
        playerCount.Add(player); // Probably doesnt do anything
        // Character Positions
        // Character turn order
        //InventorySetup(player);
    }

    private void InventorySetup(IPlayer player)
    {
        //InventoryManager.Instance.AddInventory(player.playerId, new Inventory());
    }

    //public void ActivateTile(tile)
    //{
    //    currentRoute[tile].
    //}

    public void AddDice(IDice dice)
    {
        dices.Add(dice);
    }

    public void RemoveDice(IDice dice)
    {
        dices.Remove(dice);
    }

    public void StartDice(bool state)
    {
        foreach (var die in dices)
        {
            die.SetDiceState(state);
        }
    }
}
