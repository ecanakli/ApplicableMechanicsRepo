using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TapSort : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    [Header("To Control Object Situation")]
    [SerializeField] private bool isHolded;
    [SerializeField] private bool isPlaced;

    //------------------Objects--------------------------------
    private GameObject popedObject;

    //------------------Lists----------------------------------
    private List<List<GameObject>> boxLists = new List<List<GameObject>>();

    void Start()
    {
        FillList();

        //Subscribe Event
        GameOverHandler.GameOver += HandleGameOver;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TapSorting();
        }
    }

    //Creating And Filling List With Balls
    private void FillList()
    {
        AddEmptyList();
        AddBallsToTheList();
    }

    //Create Empty Lists As Many As The Number Of Boxes
    private void AddEmptyList()
    {
        for (int i = 0; i < 5; i++)
        {
            boxLists.Add(new List<GameObject>());
        }
    }

    //Addin All Balls Into The List
    private void AddBallsToTheList()
    {

        foreach (GameObject balls in GameObject.FindGameObjectsWithTag("Ball"))
        {

            switch (balls.GetComponent<BallScript>().GetBoxNumber())
            {
                case 0:
                    boxLists[0].Add(balls);
                    break;
                case 1:
                    boxLists[1].Add(balls);
                    break;
                case 2:
                    boxLists[2].Add(balls);
                    break;
                case 3:
                    boxLists[3].Add(balls);
                    break;
                case 4:
                    boxLists[4].Add(balls);
                    break;
            }
        }
    }

    //Sorting Objects
    private void TapSorting()
    {
        PopAndPush();
        CheckIsPlaced();
    }

    //Popping Or Pushing Tapped Object
    private void PopAndPush()
    {
        Vector2 mouseSP = Input.mousePosition;
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(mouseSP);

        //---------------Push-------------

        if (isHolded && popedObject && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                Vector3 pos = hit.transform.position;
                int indexNum = hit.transform.gameObject.GetComponent<BoxScript>().index;
                if (boxLists[indexNum].Count < 4)
                {
                    popedObject.transform.position = new Vector3(pos.x, getYPos(boxLists[indexNum].Count), popedObject.transform.position.z);
                    isPlaced = true;
                    boxLists[indexNum].Add(popedObject);
                }
            }

        }

        //---------------Pop---------------

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            if (!isHolded && !popedObject)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    int indexNum = hit.transform.gameObject.GetComponent<BoxScript>().index;
                    if (boxLists[indexNum].Count > 0)
                    {
                        Vector3 upPos = new Vector3(hit.transform.position.x, 3.5f, 0.5f);
                        popedObject = boxLists[indexNum][boxLists[indexNum].Count - 1];
                        popedObject.transform.position = upPos;
                        boxLists[indexNum].RemoveAt(boxLists[indexNum].Count - 1);
                        isHolded = true;
                    }
                }
            }
        }
    }

    //To Reset PoppedObject If We Placed
    private void CheckIsPlaced()
    {
        if (popedObject && isPlaced)
        {
            isPlaced = false;
            popedObject = null;
            isHolded = false;
        }
    }

    //Get The Y Position Of Ball Inside The Box
    private float getYPos(int size)
    {
        if (size < 4)
        {
            return size * 0.8f + 0.35f;
        }
        else
        {
            return 0;
        }
    }


    //When The Game is Over Disable This Script
    private void HandleGameOver()
    {
        enabled = false;
    }
}