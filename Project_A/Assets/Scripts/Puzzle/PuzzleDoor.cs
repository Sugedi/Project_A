using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    public MatchingPuzzle redRock;
    public GameObject redTile;

    public MatchingPuzzle yellowRock;
    public GameObject yellowTile;

    public MatchingPuzzle blueRock;
    public GameObject blueTile;


    public bool redMatched = false;
    public bool yellowMatched = false;
    public bool blueMatched = false;


    // Start is called before the first frame update
    void Start()
    {
        redRock.target = redTile;
        //yellowRock.target = yellowTile;
        //blueRock.target = blueTile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBool(MatchingPuzzle gameObject)
    {
        if(gameObject == redRock)
        {
            if (!redMatched)
            {
                redMatched = true;
            }
            else
            {
                redMatched = false;
            }
        }
        else if(gameObject == yellowRock)
        {
            if (!yellowMatched)
            {
                yellowMatched = true;
            }
            else
            {
                yellowMatched = false;
            }
        }
        else if (gameObject == blueRock)
        {
            if (!blueMatched)
            {
                blueMatched = true;
            }
            else
            {
                blueMatched = false;
            }
        }

        OpenCheck();
    }


    void OpenCheck()
    {
        if(redMatched && yellowMatched && blueMatched)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    
}
