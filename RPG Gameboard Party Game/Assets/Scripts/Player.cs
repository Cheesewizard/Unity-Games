using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Route currentRoute;

    private int routePosition;

    public int steps;

    private bool isMoving = false;

    public float speed = 8f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            steps = Random.Range(1, 7);
            Debug.Log("Dice Roll = " + steps);

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


        while (steps > 0)
        {
            routePosition++;
            routePosition %= currentRoute.childNodesList.Count;

            var nextPos = currentRoute.childNodesList[routePosition].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            steps--;
        }

        isMoving = false;
    }

    private bool MoveToNextNode(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime));
    }

}
