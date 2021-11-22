using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody2D myRigidBody;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timetToMove;
    private float timeToMoveCounter;

    public float waitToReload;


    private Vector3 moveDirection;
    private bool reloading;
    private GameObject thePlayer;

	// Use this for initialization
	void Start () {

        myRigidBody = GetComponent<Rigidbody2D>();

        // timeBetweenMoveCounter = timeBetweenMove;
        // timeToMoveCounter = timetToMove;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            myRigidBody.velocity = moveDirection;

            if(timeToMoveCounter < 0f)
            {
                moving = false;
                // timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            myRigidBody.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0)
            {
                moving = true;
                // timeToMoveCounter = timetToMove;
                timeToMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }

        if (reloading)
        {
            waitToReload -= Time.deltaTime;
            if(waitToReload < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                thePlayer.SetActive(true);
            }
        }
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
       /* if(collision.gameObject.name == "Player")
        {
            // Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);

            reloading = true;

            thePlayer = collision.gameObject;
        } */
    }
}
