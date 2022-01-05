using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsRevealed { get; set; } = false;
    public bool IsBomb { get; set; } = false;
    public bool IsFlag { get; set; } = false;
    public int TotalBombs { get; set; } = 0;


    private ITile tile;

    public void GetState()
    {
        GetComponent<ITile>().Reveal();
    }
}
