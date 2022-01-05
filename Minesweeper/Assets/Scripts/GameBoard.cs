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
        // This implementation could try too add a bomb to the same tile resulting in incorect number of bombs placed
        for (int x = 0; x < mines; x++)
        {
            var col = Random.Range(0, cols);
            var row = Random.Range(0, rows);

            tiles[col, row].AddComponent<Bomb>();
        }
    }

    public void GenerateNumbers(GameObject[,] tiles, int rows, int cols)
    {
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var bombTotal = 0;

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
                            if (neighbour.GetComponent<ITile>()?.GetType() == typeof(Bomb))
                            {
                                bombTotal++;
                            }
                        }
                    }
                }

                if (tiles[x, y].GetComponent<ITile>()?.GetType() != typeof(Bomb))
                {
                    var number = tiles[x, y].AddComponent<Number>();

                    // Assign text to amount of nearby bombs
                    number.SetNumberOfBombsText(bombTotal, tiles[x, y]);
                }
            }
        }
    }
}
