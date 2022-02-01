using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 1f;

    [SerializeField]
    private Vector3 axis;

    public bool rotate = true;

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(axis * spinSpeed * Time.deltaTime, Space.Self);
        }
    }
}
