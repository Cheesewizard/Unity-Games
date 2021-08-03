using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Check which object is being clicked on
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name.Contains("Tile"))
                {
                    // Check each child object of tile and turn on the correct state
                    var states = hit.transform.GetComponent<TileStates>();
                    foreach (Transform child in hit.transform)
                    {
                        // Dont execute if tile has been marked with a flag
                        if (states.IsBomb && !states.IsFlag && child.tag == "Bomb")
                        {
                            child.gameObject.SetActive(true);
                            break;
                        }

                        if (!states.IsBomb && !states.IsFlag && child.tag == "Number")
                        {
                            child.GetComponent<TextMesh>().text = states.TotalBombs.ToString();
                            child.gameObject.SetActive(true);
                            break;
                        }
                    }
                }
            }
        }
    }
}
