using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To Determine Ball Color Using Enum
public enum BallColor { Purple, Red, Blue, Yellow };
public class BallScript : MonoBehaviour
{
    public BallColor bColor;

    public int boxNumber;

    private void Start()
    {
        EqualizeIndexNumber();
    }

    //Equalizing The Index Number Of The Box We In
    private void EqualizeIndexNumber()
    {
        boxNumber = gameObject.GetComponentInParent<BoxScript>().index;
    }

    //To Reach And Adding Balls The List It Belongs To
    public int GetBoxNumber()
    {
        return boxNumber;
    }
}