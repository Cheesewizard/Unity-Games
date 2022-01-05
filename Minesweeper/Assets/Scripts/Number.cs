using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour, ITile
{
    // Start is called before the first frame update
    private GameObject textHolder;

    public void Reveal()
    {
        textHolder.SetActive(true);
    }

    public void SetNumberOfBombsText(int NumberOfBombs, GameObject tile)
    {
        textHolder = new GameObject("Text");
        textHolder.SetActive(false);
        textHolder.AddComponent<TextMesh>();
        textHolder.GetComponent<TextMesh>().text = NumberOfBombs.ToString();

        // Add the text as a child and Set the text location to the cell
        textHolder.transform.position = tile.transform.position;
        textHolder.transform.SetParent(tile.transform);  
    }
}
