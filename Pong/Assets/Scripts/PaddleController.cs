using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{

    public float speed;

    public float Direction;

    public float adjustSpeed;

    public bool isPlayerOne;

    private Rigidbody2D paddleRb;

    // Use this for initialization
    void Start()
    {
        paddleRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayerOne)
        {
            if (Input.GetKey(KeyCode.W))
            {
                // Movement Code
                paddleRb.MovePosition(new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime)));
                Direction = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                paddleRb.MovePosition(new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime)));
                Direction = -1;
            }
            else
            {
                Direction = 0;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                // Movement Code
                paddleRb.MovePosition(new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime)));
                Direction = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                paddleRb.MovePosition(new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime)));
                Direction = -1;
            }
            else
            {
                Direction = 0;
            }
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        other.rigidbody.velocity = new Vector2(other.rigidbody.velocity.x * 1.1f, other.rigidbody.velocity.y + (Direction * adjustSpeed));
        Debug.Log(other.rigidbody.velocity);
    }

}

