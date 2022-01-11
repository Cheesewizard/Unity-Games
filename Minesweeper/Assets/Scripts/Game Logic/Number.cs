using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Number : MonoBehaviour, ITile, INumber
{
    // Start is called before the first frame update
    private GameObject textHolder;
    public bool IsRevealed { get; set; }

    public void Reveal()
    {
        SetEmptyTilesColour();
        textHolder?.SetActive(true);
        IsRevealed = true;
    }

    private void SetEmptyTilesColour()
    {
        // Doesnt have a number assigned since it is 0
        if (!GetComponent<TextMeshPro>())
        {
            var renderer = GetComponent<Renderer>();
            var mat = new Material(Shader.Find("Standard"));
            mat.color = Color.white;
            renderer.material = mat;
        }
    }

    public void SetNumberOfBombsText(int NumberOfBombs, GameObject tile)
    {
        /* Create a new gameobject to attach the text to. This is now optimised in that it only attaches
        text to cells that have numbers. Before I made it so all the cells have a hidden number child which I toggled on when revealed. */
        textHolder = new GameObject("Text");
        textHolder.SetActive(false);
        textHolder.AddComponent<TextMeshPro>();

        // May need to revisit so that the font size is dynamically set depedning on screen size. Perhaps also remove the dependency on TextMeshPro
        var textMesh = textHolder.GetComponent<TextMeshPro>();
        textMesh.text = NumberOfBombs.ToString();
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = 7;
        textMesh.color = GetNumberColourForBombCount(NumberOfBombs);

        // Add the text as a child and Set the text location to the cell
        textHolder.transform.SetParent(tile.transform);
        textHolder.transform.localPosition = new Vector3(0, 1, 0);
    }

    private Color GetNumberColourForBombCount(int bombCount)
    {
        return bombCount switch
        {
            1 => Color.blue,
            2 => Color.green,
            3 => Color.yellow,
            _ => Color.red,
        };
    }
}
