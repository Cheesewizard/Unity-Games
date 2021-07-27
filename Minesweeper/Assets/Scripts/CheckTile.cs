using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Left mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var states = gameObject.GetComponent<TileStates>();
            if (states.IsBomb)
            {
                var bomb = gameObject.transform.GetChild(1);
                bomb.gameObject.SetActive(true);
                //Game over
            }

            states.IsRevealed = true;
            var number = gameObject.transform.GetChild(0);
            number.gameObject.GetComponent<TextMesh>().text = states.TotalBombs.ToString();
            number.gameObject.SetActive(true);
        }

   

    }
}
