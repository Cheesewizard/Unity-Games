using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ITile
{
    public void Reveal()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    // Start is called before the first frame update
    void Awake()
    {
        
    }

}
