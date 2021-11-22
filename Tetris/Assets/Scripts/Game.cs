using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];


    // Use this for initialization
    void Start()
    {
        SpawnNextTetrisBlock();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGrid( Movement tetrisBlock)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (grid[x,y] != null)
                {
                    if (grid[x,y].parent == tetrisBlock.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetrisBlock.transform)
        {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition (Vector2 pos)
    {
        if (pos.y > gridHeight -1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public void SpawnNextTetrisBlock()
    {
        GameObject nextTetrisBlock = (GameObject)Instantiate(Resources.Load(GetRandomTetrisBlock(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
    }

    public bool CheckIsInsidegrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    string GetRandomTetrisBlock()
    {
        int randomtetrisBlock = Random.Range(1, 8);
        string randomTetrisName = "Prefabs/ZShape";

        switch (randomtetrisBlock)
        {
            case 1:
                randomTetrisName = "Prefabs/LShape";
                break;
            case 2:
                randomTetrisName = "Prefabs/TShape";
                break;
            case 3:
                randomTetrisName = "Prefabs/ZShape";
                break;
            case 4:
                randomTetrisName = "Prefabs/LineShape";
                break;
            case 5:
                randomTetrisName = "Prefabs/BlockShape";
                break;
            case 6:
                randomTetrisName = "Prefabs/ZShape1";
                break;
            case 7:
                randomTetrisName = "Prefabs/LShape1";
                break;
        }

        return randomTetrisName;
    }
}
