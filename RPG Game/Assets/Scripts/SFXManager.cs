using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public AudioSource playerHurt;
    public AudioSource playerDead;
    public AudioSource playerAttack1;
    public AudioSource playerAttack2;
    public AudioSource playerAttack3;
    public AudioSource playerAttack4;

    public AudioSource slimeStab;
    public AudioSource slimeDie;

    private static bool sfxManagerexists;

	// Use this for initialization
	void Start () {

        if (!sfxManagerexists)
        {
            sfxManagerexists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
