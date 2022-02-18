using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cell : MonoBehaviour
{
    public bool state = false;
    public int liveNeighbours = 0;

    public void SetCellState()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < 60)
        {
            state = true;
            setSprite();
        }
        else
        {
            setSprite();
        }
    }

    public void setSprite()
    {
        if (state)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
