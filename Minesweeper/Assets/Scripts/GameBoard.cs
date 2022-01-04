using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour, IGameBoard
{
    public GameObject[,] CreateBoard(GameObject tile, float width, int rows, int cols, int start_x, int start_y)
    {
        var tiles = new GameObject[rows, cols];

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var posx = x * width;
                var posy = y * width;
                tiles[x, y] = Instantiate(tile, new Vector3(start_x + posx, -start_y + posy), Quaternion.Euler(90, 0, 0));
            }
        }

        return tiles;
    }

    public void CreateMines(GameObject[,] tiles, int mines, int rows, int cols)
    {
        for (int x = 0; x < mines; x++)
        {
            var col = Random.Range(0, cols);
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
            CreateMines(tiles, mines, rows, cols);
        }
    }

    public void GenerateNumbers(GameObject[,] tiles, int rows, int cols)
    {
        for (int x = 0; x < cols; x++)
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

                        if (i > -1 && i < cols && j > -1 && j < rows)
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
                    // Assign text to amount of nearby bombs
                    tiles[x, y].GetComponent<TileStates>().TotalBombs = total;
                    //number.GetComponent<TextMesh>().text = total.ToString();
                    total = 0;
                    //Instantiate(number, new Vector3(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y, -5), Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }
}
