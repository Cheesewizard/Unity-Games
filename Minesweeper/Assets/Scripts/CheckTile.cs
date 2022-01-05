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
        RayCastCell();
    }

    private void RayCastCell()
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
                    hit.transform.GetComponent<ITile>().Reveal();
                }
            }
        }
    }
}
