using System.Collections;
using System.Collections.Generic;
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


    public TextMeshPro userInterface;

    public void UpdateMovement(int stepsLeft)
    {
        if (stepsLeft <= 0)
        {
            userInterface.text = null;
            return;
        }

        userInterface.text = stepsLeft.ToString();
    }
}
