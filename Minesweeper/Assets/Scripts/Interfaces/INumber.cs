using UnityEngine;

public interface INumber
{
    bool IsRevealed { get; set; }

    void Reveal();
    void SetNumberOfBombsText(int NumberOfBombs, GameObject tile);
}