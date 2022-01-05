using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFlag : MonoBehaviour
{
    public LayerMask ignore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Right mouse click
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            // Check which object is being clicked on
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignore))
            {
                if (hit.transform.name.Contains("Tile"))
                {
                    // Check each child object of tile and turn on the correct state
                    var states = hit.transform.GetComponent<TileState>();
                    foreach (Transform child in hit.transform)
                    {
                        // Dont execute if tile has been marked with a flag
                        if (child.tag == "Flag")
                        {
                            // Toggle the flag 
                            if (states.IsFlag)  // Add a check here so you cannot set a flag on a visible tile. 
                            {
                                child.gameObject.SetActive(false);
                            }
                            else
                            {
                                child.gameObject.SetActive(true);
                            }

                            states.IsFlag = !states.IsFlag;
                            continue;
                        }

                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
