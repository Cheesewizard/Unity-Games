using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour, IFlag
{
    private void SetFlag(GameObject tile)
    {
        tile.AddComponent<Flag>();
    }

    private void RemoveFlag(GameObject tile)
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
