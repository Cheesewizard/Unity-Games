using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, ITile
{
    public void Reveal()
    {
        var renderer = GetComponent<Renderer>();
        var materials = renderer.materials;

        var bomb = Resources.Load<Material>("Materials/Bomb");
        renderer.materials = new Material[] { materials[0], bomb };
    }
}
