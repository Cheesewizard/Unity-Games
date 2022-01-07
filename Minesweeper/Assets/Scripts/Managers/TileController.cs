using UnityEngine;

public class TileController : MonoBehaviour
{
    private IFlag flagController;
    void Awake()
    {
        flagController = GetComponent<IFlag>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCell();
    }

    private void CheckCell()
    {
        // Think of a better way to remove duplicate code....
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            var hit = GetTile();
            RevealTile(hit);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            var hit = GetTile();
            flagController.CheckForUpdate(hit?.gameObject);
        }
    }

    //private void FloodFIllReveal()
    //{
    //    var hit = GetTile();

    //    for (int x = 0; x < cols; x++)
    //    {
    //        for (int y = 0; y < rows; y++)
    //        {
    //            var bombTotal = 0;

    //            // Check neighbours
    //            for (int xoff = -1; xoff <= 1; xoff++)
    //            {
    //                for (int yoff = -1; yoff <= 1; yoff++)
    //                {
    //                    // Stops out of range error for tiles on the edge
    //                    var i = x + xoff;
    //                    var j = y + yoff;

    //                    if (i > -1 && i < cols && j > -1 && j < rows)
    //                    {
    //                        var neighbour = hit.[i, j];
    //                        if (neighbour.GetComponent<ITile>()?.GetType() == typeof(Number))
    //                        {
    //                            bombTotal++;
    //                        }
    //                    }
    //                }
    //            }

    //        }
    //    }
    //}

    private void RevealTile(Transform tile)
    {
        // Dont allow if tile has been flagged
        if (!tile?.GetComponent<Flag>())
        {
            tile?.GetComponent<ITile>().Reveal();
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
