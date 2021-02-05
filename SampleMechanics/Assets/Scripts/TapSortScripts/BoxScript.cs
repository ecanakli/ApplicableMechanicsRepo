using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    //To Determine Which Box Is
    public int index;

    //----------To Count Matched Colors-----------
    private int purpleCounter;
    private int redCounter;
    private int blueCounter;
    private int yellowCounter;

    //----------To Count Matched Balls-----------
    private bool purpleMatch;
    private bool redMatch;
    private bool blueMatch;
    private bool yellowMatch;

    //Cheking All Balls Matched Or Not
    private bool isAllMatched;

    //Calculating Ball Color When Putting Object Into The Box
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Purple)
        {
            purpleCounter++;
        }
        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Red)
        {
            redCounter++;
        }
        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Blue)
        {
            blueCounter++;
        }
        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Yellow)
        {
            yellowCounter++;
        }

        CheckIsMatched();
    }

    //Calculating Ball Color When Popping Object Inside Of The Box
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Purple)
        {
            purpleCounter--;
        }

        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Red)
        {
            redCounter--;
        }

        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Blue)
        {
            blueCounter--;
        }

        if (other.gameObject.GetComponent<BallScript>().bColor == BallColor.Yellow)
        {
            yellowCounter--;
        }

        CheckIsMatched();
    }

    //Checking Do All The Ball Colors Inside Of The Box Is Matched
    private void CheckIsMatched()
    {
        if (purpleCounter == 4)
        {
            Debug.Log("Purple Match");
            purpleMatch = true;
        }
        else
        {
            purpleMatch = false;
        }

        if (redCounter == 4)
        {
            Debug.Log("Red Match");
            redMatch = true;
        }
        else
        {
            redMatch = false;
        }

        if (blueCounter == 4)
        {
            Debug.Log("Blue Match");
            blueMatch = true;
        }
        else
        {
            blueMatch = false;
        }

        if (yellowCounter == 4)
        {
            Debug.Log("Yellow Match");
            yellowMatch = true;
        }
        else
        {
            yellowMatch = false;
        }
    }

    //To Find Out If The Balls Inside Of The Box Are Matched
    public bool GetIsMatched()
    {
        if (purpleMatch || redMatch || blueMatch || yellowMatch == true)
        {
            isAllMatched = true;
        }
        else
        {
            isAllMatched = false;
        }

        EmptyList();

        return isAllMatched;
    }

    //If The List Is Empty
    private void EmptyList()
    {
        if (purpleCounter == 0 && redCounter == 0 && blueCounter == 0 && yellowCounter == 0)
        {
            isAllMatched = true;
        }
    }
}