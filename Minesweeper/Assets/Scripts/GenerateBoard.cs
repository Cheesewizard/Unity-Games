using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{

    public int columns;
    public int rows;
    public float width;

    public float start_x, start_y;
    public int minesTotal = 10;

    public GameObject tile;
    public GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[columns, rows];

        CreateBoard();
        CreateMines(minesTotal);
        GenerateNumbers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateBoard()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var posx = x * width;
                var posy = y * width;
                tiles[x, y] = Instantiate(tile, new Vector3(start_x + posx, -start_y + posy), Quaternion.Euler(90, 0, 0));
            }
        }
    }

    public void CreateMines(int mines)
    {
        for (int x = 0; x < mines; x++)
        {
            var col = Random.Range(0, columns);
            var row = Random.Range(0, rows);

            if (tiles[col, row].GetComponent<TileStates>().IsBomb)
            {
                break;
            }

            mines--;
            tiles[col, row].GetComponent<TileStates>().IsBomb = true;
            //Instantiate(bomb, new Vector3(tiles[col, row].transform.position.x, tiles[col, row].transform.position.y, -5), Quaternion.Euler(-90, 0, 0));
        }

        if (mines > 0)
        {
            // Reccursive, run until all mines have been placed. Fires if a cell already contains a bomb.
            CreateMines(mines);
        }
    }

    public void GenerateNumbers()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var total = 0;

                // Check neighbours
                for (int xoff = -1; xoff <= 1; xoff++)
                {
                    for (int yoff = -1; yoff <= 1; yoff++)
                    {
                        // Stops out of range error for tiles on the edge
                        var i = x + xoff;
                        var j = y + yoff;

                        if (i > -1 && i < columns && j > -1 && j < rows)
                        {
                            var neighbour = tiles[i, j];
                            if (neighbour.GetComponent<TileStates>().IsBomb)
                            {
                                total++;
                            }
                        }
                    }
                }

                if (!tiles[x, y].GetComponent<TileStates>().IsBomb)
                {
                    tiles[x, y].GetComponent<TileStates>().TotalBombs = total;
                    //number.GetComponent<TextMesh>().text = total.ToString();
                    total = 0;
                    //Instantiate(number, new Vector3(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y, -5), Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }
}
