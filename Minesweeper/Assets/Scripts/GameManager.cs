using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int columns;
    public int rows;
    public float width = 1;

    private int start_x, start_y;
    public int minesTotal = 1;

    public GameObject tile;
    public GameObject[,] tiles;

    private IGameBoard gameBoard;

    public GameObject cameraObject;
    private ICamera camera;

    // Start is called before the first frame update
    void Start()
    {
        width = MapWidthDefaultValue(width);
        //SetOffsets();

        camera = cameraObject.GetComponent<ICamera>();
        gameBoard = this.GetComponent<IGameBoard>();

        tiles = gameBoard.CreateBoard(tile, width, rows, columns, start_x, start_y);
        gameBoard.CreateMines(tiles, minesTotal, rows, columns);
        gameBoard.GenerateNumbers(tiles, rows, columns);
        camera.CentreCamera(cameraObject, rows, columns, width);

        // gameBoard.transform.position = 

        tiles[5, 5].transform.position = new Vector3(tiles[5, 5].transform.position.x, tiles[5, 5].transform.position.y, -100);
    }

    private float MapWidthDefaultValue(float width)
    {
        return width * 1.5f;
    }

    private void SetOffsets()
    {
        start_x = columns / 2;
        start_y = columns / 2;
    }
}
