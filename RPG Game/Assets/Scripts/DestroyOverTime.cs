using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

    public float timetoDestory;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timetoDestory -= Time.deltaTime;

        if(timetoDestory <= 0)
        {
            Destroy(gameObject);
        }
	}
}
