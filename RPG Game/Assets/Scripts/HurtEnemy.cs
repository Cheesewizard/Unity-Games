using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    private int currentDamage;
    public int damagetoGive;
    public GameObject damageHit;
    public Transform hitPoint;
    public GameObject damageNumber;

    private PlayerStats thePS;

    private SFXManager sfxMan;


	// Use this for initialization
	void Start () {
        thePS = FindObjectOfType<PlayerStats>();
        sfxMan = FindObjectOfType<SFXManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // Destroy(other.gameObject);

            currentDamage = damagetoGive + thePS.currentAttack;

            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);
            Instantiate(damageHit, hitPoint.position, hitPoint.rotation);
            var clone = (GameObject) Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));
            sfxMan.slimeStab.Play();
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
;        }
    }
}
