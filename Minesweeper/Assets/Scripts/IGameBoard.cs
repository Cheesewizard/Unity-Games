using UnityEngine;

public interface IGameBoard
{
    GameObject[,] CreateBoard(GameObject tile, float width, int rows, int cols, int start_x, int start_y);
    void CreateMines(GameObject[,] tiles, int mines, int rows, int cols);
    void GenerateNumbers(GameObject[,] tiles, int rows, int cols);
}