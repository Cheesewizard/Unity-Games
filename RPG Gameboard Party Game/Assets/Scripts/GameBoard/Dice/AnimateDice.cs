using System;
using UnityEngine;

public class AnimateDice : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 1f;

    [SerializeField] private Vector3 axis;
    public bool rotate;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (rotate)
        {
            transform.Rotate(axis * spinSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void SetDiceToNumber(int diceNumber)
    {
        rotate = false;
        switch (diceNumber)
        {
            case 1:
                transform.rotation = Quaternion.Euler(180, 0, 0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(270, 0, 0);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case 4:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 5:
                transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case 6:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }
}