using UnityEngine;

public class Flag : MonoBehaviour
{
    void Awake()
    {
        var renderer = GetComponent<Renderer>();
        var materials = renderer.materials;

        var flag = Resources.Load<Material>("Materials/Flag");
        renderer.materials = new Material[] { materials[0], flag };
    }

    void OnDestroy()
    {
        var renderer = GetComponent<Renderer>();
        var materials = renderer.materials;
        renderer.materials = new Material[] { materials[0] };
    }
}
