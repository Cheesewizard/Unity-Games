using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int routePosition;
    private bool isMoving = false;

    [SerializeField]
    private int steps;
  
    public float speed = 8f;

    //Events
    public Action<bool> PlayerMoving;
    public Action<int> PlayerSteps;

    private void OnEnable()
    {
        PlayerMoving += GameManager.Instance.StartDice;
        PlayerSteps += UIManager.Instance.UpdateMovement;
    }

    private void OnDisable()
    {
        PlayerMoving -= GameManager.Instance.StartDice;
        PlayerSteps -= UIManager.Instance.UpdateMovement;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            // Get the global dices managed by the game manager
            foreach (var die in GameManager.Instance.dices)
            {
                // If multiple dice are in play this will add each dice, no matter how many.
                steps += die.GetDiceNumber();
            }
           
            StartCoroutine(Move());
        }
    }


    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }


        isMoving = true;
        PlayerMoving?.Invoke(true);

        while (steps > 0)
        {
            PlayerSteps?.Invoke(steps);

            routePosition++;
            routePosition %= GameManager.Instance.currentRoute.childNodesList.Count;
           
            var nextPos = GameManager.Instance.currentRoute.childNodesList[routePosition].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(0.1f);
            steps--;
        }

        isMoving = false;
        PlayerSteps?.Invoke(steps);
        PlayerMoving?.Invoke(false);
    }

    private bool MoveToNextNode(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime));
    }

}
