using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    public List<Transform> childNodesList = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        FillNodes();

        for (int i = 0; i < childNodesList.Count; i++)
        {
            Vector3 currentPos = childNodesList[i].position;
            if (i > 0)
            {
                Vector3 prevPos = childNodesList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childNodesList.Clear();
        childObjects = GetComponentsInChildren<Transform>();

        foreach (var child in childObjects)
        {
            if (child != this.transform)
            {
                childNodesList.Add(child);
            }
        }
    }
}
