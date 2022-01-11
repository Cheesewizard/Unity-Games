using Assets.Scripts.Interfaces;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //private int _columns;
    //public int columns
    //{
    //    get { return columns; }   
    //    set { if (_columns > 50) { _columns = 50; } }  
    //}

    //private int _rows;
    //public int rows
    //{
    //    get { return rows; }
    //    set { if (_rows > 50) { _rows = 50; } }
    //}

    public int columns, rows;


    public float width = 1;
    public int minesTotal = 1;

    public GameObject tile;
    public GameObject[,] gameTiles;
    public GameObject cameraObject;

    private ILose lose;
    private IWin win;

    // Start is called before the first frame update
    void Start()
    {
        width = MapWidthDefaultValue(width);
        CheckBoardSize();

        camera = cameraObject.GetComponent<ICamera>();
        gameBoard = GetComponent<IGameBoard>();

        // Generate board
        gameTiles = gameBoard.CreateBoard(tile, width, rows, columns, start_x, start_y);
        gameBoard.CreateMines(gameTiles, minesTotal, rows, columns);
        gameBoard.GenerateNumbers(gameTiles, rows, columns);

        // Camera
        camera.CentreCamera(cameraObject, rows, columns, width);

        lose = new Lose();
        win = new Win();
    }

    // Sanity check to avoid stack overflow exception with high tile values. Could be improved to allow huge games by using a flyweight pattern.
    private void CheckBoardSize()
    {
        if (columns > 50)
        {
            columns = 50;
        }

        if (rows > 50)
        {
            rows = 50;
        }
    }

    private float MapWidthDefaultValue(float width)
    {
        // Dont allow negative values
        if (width < 1)
        {
            width = 1;
        }

        return width * 1.5f;
    }

    private void SetOffsets()
    {
        start_x = columns / 2;
        start_y = columns / 2;
    }

    public void CheckAllTilesAreRevealed()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var gameTile = gameTiles[x, y].GetComponent<ITile>();
                if (gameTile.GetType() == typeof(Number))
                {
                    if (!gameTile.IsRevealed)
                    {
                        // Still tiles yet to reveal
                        return;
                    }
                }
            }
        }

        win.Success();
    }

    public void GameOver()
    {
        lose.Failure();
    }


    private ICamera camera;
    private IGameBoard gameBoard;
    private int start_x, start_y;
}
