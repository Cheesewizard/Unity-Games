using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;

    private static bool cameraExists;

    public BoxCollider2D boundBox;
    private Vector3 minbounds;
    private Vector3 maxbounds;

    private Camera theCamera;

    private float halfHeight;
    private float halfWidth;

    // Use this for initialization
    void Start()
    {

        DontDestroyOnLoad(transform.gameObject);
        // followTarget.GetComponent<GameObject>();

        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //if (boundBox == null)
        //{
        //    boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
        //    minbounds = boundBox.bounds.min;
        //    maxbounds = boundBox.bounds.max;
        //}


        minbounds = boundBox.bounds.min;
        maxbounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        if (boundBox != null)
        {
            float clampedX = Mathf.Clamp(transform.position.x, minbounds.x + halfWidth, maxbounds.x - halfWidth);
            float clamedY = Mathf.Clamp(transform.position.y, minbounds.y + halfHeight, maxbounds.y - halfHeight);

            transform.position = new Vector3(clampedX, clamedY, transform.position.z);
        }


        //if (boundBox == null)
        //{
        //    boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
        //    minbounds = boundBox.bounds.min;
        //    maxbounds = boundBox.bounds.max;
        //}
    }

    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;

        minbounds = boundBox.bounds.min;
        maxbounds = boundBox.bounds.max;

    }
}
