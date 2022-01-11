using Assets.Scripts.Interfaces;
using UnityEngine;

public class Win : IWin
{
    public void Success()
    {
        // Game won
        Debug.Log("You Win");
    }
}
