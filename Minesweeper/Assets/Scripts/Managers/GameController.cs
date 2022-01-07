using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int columns;
    public int rows;
    public float width = 1;

    private int start_x, start_y;
    public int minesTotal = 1;

    public GameObject tile;
    public GameObject[,] tiles;
    public GameObject cameraObject;
 
    // Start is called before the first frame update
    void Start()
    {
        width = MapWidthDefaultValue(width);

        camera = cameraObject.GetComponent<ICamera>();
        gameBoard = this.GetComponent<IGameBoard>();

        // Generate board
        tiles = gameBoard.CreateBoard(tile, width, rows, columns, start_x, start_y);
        gameBoard.CreateMines(tiles, minesTotal, rows, columns);
        gameBoard.GenerateNumbers(tiles, rows, columns);

        // Camera
        camera.CentreCamera(cameraObject, rows, columns, width);
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


    private ICamera camera;
    private IGameBoard gameBoard;
}
