using UnityEngine;

public class IndexPosition : MonoBehaviour
{
    public int PosX { get; set; }
    public int PosY { get; set; }

    public void SetPostion(int x, int y)
    {
        this.PosX = x;
        this.PosY = y;
    }
}
