using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour {

    public string LevelToLoad;
    public Camera camera;
    public string exitPoint;

    private PlayerController thePlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        thePlayer = FindObjectOfType<PlayerController>();    
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(LevelToLoad);
            thePlayer.startPoint = exitPoint;
            var position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.x, camera.transform.position.z);
            camera.transform.position = position;
        }
    }
}
