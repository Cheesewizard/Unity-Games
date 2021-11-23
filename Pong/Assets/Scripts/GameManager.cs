using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;

    private int score1;
    private int score2;

    public Canvas winScreen;
    public RectTransform background;
    public Text winText;
    public GameObject ball;

    private bool isFinished;

    // Use this for initialization
    void Start()
    {
        background.localScale = new Vector3(0, 0);

    }

    void Update()
    {
        if (isFinished)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void UpdateScore(int player)
    {
        if (player == 1)
        {
            score1 += 1;
        }

        if (player == 2)
        {
            score2 += 1;
        }

        scoreText.text = score1 + " - " + score2;

        CheckIfWin();
    }

    private void CheckIfWin()
    {
        if (score1 >= 10 || score2 >= 10)
        {
            // Puase Game
            Time.timeScale = 0;


            // Win condition
            winText.text = ($"{GetWinningPlayer()} {Environment.NewLine} Wins");

            winScreen.gameObject.SetActive(true);
            winText.gameObject.SetActive(true);
            background.localScale = new Vector3(0.33f, 0.31f);

            isFinished = true;
        }
    }

    private string GetWinningPlayer()
    {
        if (score1 >= 10)
        {
            return "Player 1";
        }

        return "Player 2";
    }

    public void CreateNewBall(Vector3 position, Vector2 force)
    {
        var newBall = Instantiate(ball, position, ball.transform.rotation);
        newBall.GetComponent<Rigidbody2D>().velocity = force;
    }
}
