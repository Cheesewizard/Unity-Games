using Assets.Scripts.Interfaces;
using UnityEngine;

public class Lose : ILose
{
    public void Failure()
    {
        // Game Lost
        Debug.Log("You Lose");
    }
}
