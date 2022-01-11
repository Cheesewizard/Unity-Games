using UnityEngine;

public interface IFlag
{
    void RemoveFlag(GameObject tile);
    void CheckForUpdate(GameObject tile);
}