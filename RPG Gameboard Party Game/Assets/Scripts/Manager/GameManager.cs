using System.Collections;
using System.Collections.Generic;
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

    [SerializeReference]
    public List<IDice> dices;
    public Route currentRoute;

    // Start is called before the first frame update

    // Switch players, player camera

    // Inventory, roll dice?

    // Roll Dice, animate, get number

    // Move


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
