using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderDetector : MonoBehaviour
{
    private bool isTravelling;
    private SpriteRenderer sR;
    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();

        //Subscribing Event
        GameOverManager.IsGameOver += HandleGameOver;
    }
    private void OnDestroy()
    {
        //Unsubscribing Event
        GameOverManager.IsGameOver -= HandleGameOver;
    }

    private void OnTriggerStay(Collider other)
    {
        //When We Released The Box
        if (other.CompareTag("Box") && other.GetComponent<PlaceableObject>().isHold == false && isTravelling == false)
        {
            //Placing The Box To The Right Position
            StartCoroutine(Placing(other.transform, transform));
            //To Showing Player The Box Is In The Right Position
            sR.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If We Pull Out The Box From The Right Position
        if (other.CompareTag("Box"))
        {
            sR.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //To Showing Player The Box Is In The Right Position
        if (other.CompareTag("Box"))
        {
            sR.color = Color.green;
        }
    }

    IEnumerator Placing(Transform boxTransform, Transform targetTransform)
    {
        //To Detect Is The Object Is Travelling(To Avoid RePlacing)
        isTravelling = true;

        //Travelling Process
        while (Vector3.Distance(boxTransform.position, targetTransform.position) > 0.5f)
        {
            yield return new WaitForEndOfFrame();
            boxTransform.position = Vector3.MoveTowards(boxTransform.position, targetTransform.position, 0.1f);
        }

        isTravelling = false;
        boxTransform.GetComponent<Rigidbody>().isKinematic = true;

        //After Placement Process, Set The Box Is In Right Place And Add To The List
        if (boxTransform.GetComponent<PlaceableObject>().isFilled == false)
        {
            boxTransform.transform.parent = transform;
            boxTransform.parent.parent.GetComponent<ReplacementController>().AddNewItem(boxTransform.gameObject);
            boxTransform.GetComponent<PlaceableObject>().isFilled = true;
        }
    }

    //When Game Ends Disable This Script
    private void HandleGameOver()
    {
        enabled = false;
    }
}
