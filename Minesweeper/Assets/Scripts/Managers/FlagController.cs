using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour, IFlag
{
    private void SetFlag(GameObject selectedTile)
    {
        var tile = selectedTile.GetComponent<ITile>();

        if (tile != null)
        {
            if (!tile.IsRevealed)
            {
                selectedTile.AddComponent<Flag>();
            }
        }
    }

    public void RemoveFlag(GameObject tile)
    {
        Destroy(tile.GetComponent<Flag>());
    }

    public void CheckForUpdate(GameObject tile)
    {
        if (tile != null)
        {
            if (tile.GetComponent<Flag>())
            {
                RemoveFlag(tile.gameObject);
                return;
            }

            SetFlag(tile.gameObject);
        }
    }
}
