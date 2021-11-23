using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballcontroller : MonoBehaviour
{

    public float startForce;

    private Rigidbody2D ballRb;

    private GameObject paddle1;
    private GameObject paddle2;

    public GameObject ball;

    private GameManager gm;

    // Use this for initialization
    void Start()
    {
        paddle1 = GameObject.FindGameObjectWithTag("player1");
        paddle2 = GameObject.FindGameObjectWithTag("player2");
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        ballRb = GetComponent<Rigidbody2D>();
        ballRb.velocity = new Vector2(startForce, startForce);
    }

    void OnTriggerEnter2D(Collider2D others)
    {
        Destroy(ball.gameObject);

        if (others.CompareTag("goalzone2"))
        {
            gm.UpdateScore(1);
            gm.CreateNewBall(paddle1.transform.position + new Vector3(1f, 0, 0), new Vector2(-startForce, -startForce));
        }
        else if (others.CompareTag("goalZone1"))
        {
            gm.UpdateScore(2);
            gm.CreateNewBall(paddle2.transform.position + new Vector3(-1f, 0, 0), new Vector2(-startForce, startForce));

        }
    }
}
