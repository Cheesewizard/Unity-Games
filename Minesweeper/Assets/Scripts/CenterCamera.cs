using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCamera : MonoBehaviour, ICamera
{
    public void CentreCamera(GameObject camera, int rows, int columns, float width)
    {
        camera.transform.position = new Vector3((columns + (width * columns) / 2), rows + (width * rows) / 2, this.transform.position.z);
    }
}
