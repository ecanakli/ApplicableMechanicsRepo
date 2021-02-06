using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum colorType { Purple, Red, Blue};
public class PlaceableObject : MonoBehaviour
{
    public colorType boxColor;
    public bool isHold;
    public bool isFilled;
}
