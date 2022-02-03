using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
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


    public TextMeshPro playerSteps;
    public TextMeshPro playerCoins;

    private string MoneyHeader = "Coins: ";

    public void UpdateMovement(int stepsLeft)
    {
        if (stepsLeft <= 0)
        {
            playerSteps.text = null;
            return;
        }

        playerSteps.text = stepsLeft.ToString();
    }

    public void UpdateMoney(int money)
    {
        if (money <= 0)
        {
            playerCoins.text = $"{MoneyHeader}{0}";
            return;
        }

        playerCoins.text = $"{MoneyHeader}{money}";
    }
}
