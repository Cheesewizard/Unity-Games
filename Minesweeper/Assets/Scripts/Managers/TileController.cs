using UnityEngine;

public class TileController : MonoBehaviour
{
    private IFlag flagController;
    private GameObject[,] gameTiles;
    private int columns, rows;

    void Awake()
    {
        flagController = GetComponent<IFlag>();
    }

    // There is a chance this design choice could run out of order cuasing a null reference, needs a different design.
    void Start()
    {
        gameTiles = GameController.Instance.gameTiles;
        columns = GameController.Instance.columns;
        rows = GameController.Instance.rows;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCell();
    }

    private void CheckCell()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            var hit = GetTile();

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                RevealTile(hit);

                // Game Over
                if (hit?.GetComponent<ITile>().GetType() == typeof(Bomb))
                {
                    // Dont allow if tile has been flagged
                    if (!hit?.GetComponent<Flag>())
                    {
                        GameController.Instance.GameOver();
                        return;
                    }
                }

                GameController.Instance.CheckAllTilesAreRevealed();
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                flagController.CheckForUpdate(hit?.gameObject);
            }
        }
    }

    // Recursive function
    private void FloodFIllReveal(int x, int y, bool[,] visited)
    {
        //check x and y is in range
        if (x >= 0 && y >= 0 && x < columns && y < rows)
        {
            // already vistiied the tile
            if (visited[x, y])
            {
                return;
            }

            // Remove a flag on a tile if it is going to be revealed.
            flagController.RemoveFlag(gameTiles[x, y].gameObject);

            // Reveal the tile if its not been checked before
            gameTiles[x, y].GetComponent<ITile>().Reveal();

            // Stop if adjacent tile is a bomb
            if (CheckForAdjacentMines(x, y) > 0)
            {
                return;
            }

            // Mark as visited to stop checking the same tile
            visited[x, y] = true;

            // Check all 8 tiles around x, y
            FloodFIllReveal(x - 1, y, visited);
            FloodFIllReveal(x + 1, y, visited);
            FloodFIllReveal(x, y - 1, visited);
            FloodFIllReveal(x, y + 1, visited);
            FloodFIllReveal(x - 1, y - 1, visited);
            FloodFIllReveal(x + 1, y + 1, visited);
            FloodFIllReveal(x + 1, y - 1, visited);
            FloodFIllReveal(x - 1, y + 1, visited);
        }
    }

    // Similar code exists within 'GameBoard.cs' could try and make more generic to reuse the code.
    public int CheckForAdjacentMines(int x, int y)
    {
        int count = 0;

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
                    var neighbour = gameTiles[i, j];
                    if (neighbour.GetComponent<ITile>()?.GetType() == typeof(Bomb))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private void RevealTile(Transform tile)
    {
        // Dont allow if tile has been flagged
        if (!tile?.GetComponent<Flag>())
        {
            tile?.GetComponent<ITile>().Reveal();
            ReavealNeighbour(tile);
        }
    }

    private void ReavealNeighbour(Transform tile)
    {
        if (tile?.GetComponent<ITile>()?.GetType() == typeof(Number))
        {
            // This class is added to the tiles when the board is generated. It contains the coordinates for its position in the board.
            var index = tile.GetComponent<IndexPosition>();

            // Recursive function
            FloodFIllReveal(index.PosX, index.PosY, new bool[rows, columns]);
        }
    }

    private Transform GetTile()
    {
        // Check which object is being clicked on
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name.Contains("Tile"))
            {
                return hit.transform;
            }
        }

        return null;
    }
}

