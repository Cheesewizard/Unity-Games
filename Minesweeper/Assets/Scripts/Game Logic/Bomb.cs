using UnityEngine;

public class Bomb : MonoBehaviour, ITile, IBomb
{
    public bool IsRevealed { get; set; }

    public void Reveal()
    {
        IsRevealed = true;
        var renderer = GetComponent<Renderer>();
        var materials = renderer.materials;

        var bomb = Resources.Load<Material>("Materials/Bomb");
        renderer.materials = new Material[] { materials[0], bomb };
    }
}
