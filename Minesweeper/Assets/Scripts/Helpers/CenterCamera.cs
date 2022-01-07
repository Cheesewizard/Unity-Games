using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCamera : MonoBehaviour, ICamera
{
    public void CentreCamera(GameObject camera, int rows, int columns, float width)
    {
        //camera.transform.position = new Vector3((columns + (width * columns) / 2), rows + (width * rows) / 2, this.transform.position.z);
        //camera.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width /2 + (columns * (width * columns)) , Screen.height , Camera.main.nearClipPlane));
    }
}
