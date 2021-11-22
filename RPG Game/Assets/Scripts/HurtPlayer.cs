using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;
    private int currentDamage;

    public GameObject damageNumber;
    private PlayerStats thePS;

    private FloatingNumbers theFN;

	// Use this for initialization
	void Start () {
        thePS = FindObjectOfType<PlayerStats>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
         {

            currentDamage = damageToGive - thePS.currentDefence;
            if(currentDamage < 0)
            {
                currentDamage = 1;
            }

            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage);

            
            var clone = (GameObject)Instantiate(damageNumber, collision.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
          


        } 
    }
}
