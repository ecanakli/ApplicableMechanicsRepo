using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacementController : MonoBehaviour
{
    [SerializeField] private List<GameObject> replacement = new List<GameObject>();

    //----------To Count Matched Colors-----------
    private int purpleCounter;
    private int redCounter;
    private int blueCounter;

    //----------To Count Matched Boxes-----------
    private bool purpleMatch;
    private bool redMatch;
    private bool blueMatch;

    //Cheking All Boxes Matched Or Not
    private bool isAllMatched;

    //Add Collided Box To The List
    public void AddNewItem(GameObject newItem)
    {
        if (replacement.Count < 4)
        {
            replacement.Add(newItem);

            if (newItem.GetComponent<PlaceableObject>().boxColor == colorType.Blue)
            {
                blueCounter++;
            }

            if (newItem.GetComponent<PlaceableObject>().boxColor == colorType.Red)
            {
                redCounter++;
            }

            if (newItem.GetComponent<PlaceableObject>().boxColor == colorType.Purple)
            {
                purpleCounter++;
            }
        }

        CheckIsMatched();
    }

    //Remove Collided Box From The List
    public void RemoveItem(GameObject DeleteItem)
    {
        replacement.Remove(DeleteItem);

        if (DeleteItem.GetComponent<PlaceableObject>().boxColor == colorType.Blue)
        {
            blueCounter--;
        }

        if (DeleteItem.GetComponent<PlaceableObject>().boxColor == colorType.Red)
        {
            redCounter--;
        }

        if (DeleteItem.GetComponent<PlaceableObject>().boxColor == colorType.Purple)
        {
            purpleCounter--;
        }

        CheckIsMatched();
    }

    //Checking Do All The Box Colors Inside Of The Replacement Is Matched
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
    }

    //To Find Out If The Boxes Inside Of The Replacement Are Matched
    public bool GetIsMatched()
    {
        if (purpleMatch || redMatch || blueMatch == true)
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
        if (purpleCounter == 0 && redCounter == 0 && blueCounter == 0)
        {
            isAllMatched = true;
        }
    }
}
