using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2;
    private float currentMoveSpeed;

    // public float diagonalSpeedModifier;

    private Animator anim;
    private bool playerMoving;
    public Vector2 lastMove;
    private Vector2 moveInput;

    private Rigidbody2D myrigidbody;
    private static bool playerExists;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public string startPoint;

    public bool canMove;

    private SFXManager sfxMan;

    private int attackSound;

    // Use this for initialization
    void Start()
    {
        // lastMove = new Vector2(0, -1);

        anim = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody2D>();
        sfxMan = FindObjectOfType<SFXManager>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        canMove = true;


    }

    // Update is called once per frame
    void Update()
    {
        attackSound = Random.Range(1, 5);

        playerMoving = false;

        if (!canMove)
        {
            myrigidbody.velocity = Vector2.zero;
            return;
        }

        if (!attacking)
        {
            /*
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                // transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                myrigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, myrigidbody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

            }
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                // transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);

                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5 && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                myrigidbody.velocity = new Vector2(0f, myrigidbody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5 && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, 0f);
            } */

            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (moveInput != Vector2.zero)
            {
                myrigidbody.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                playerMoving = true;
                lastMove = moveInput;
            }
            else
            {
                myrigidbody.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                attackTimeCounter = attackTime;
                attacking = true;
                myrigidbody.velocity = Vector2.zero;
                anim.SetBool("Attack", true);

                switch (attackSound)
                {
                    case 1:
                        sfxMan.playerAttack1.Play();
                        break;
                    case 2:
                        sfxMan.playerAttack2.Play();
                        break;
                    case 3:
                        sfxMan.playerAttack3.Play();
                        break;
                    case 4:
                        sfxMan.playerAttack4.Play();
                        break;
                }





            }

            /*  if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
              {
                  currentMoveSpeed = moveSpeed * diagonalSpeedModifier;

              }
              else
              {
                  currentMoveSpeed = moveSpeed;
              } */



        }


        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if (attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("Attack", false);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

        // Debug.Log(playerMoving);

    }
}
