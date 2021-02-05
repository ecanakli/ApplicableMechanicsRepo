using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public static event Action GameOver;

    private List<BoxScript> boxList = new List<BoxScript>();

    private bool isGameOver;

    private void Start()
    {
        GetBoxes();
    }

    //Get Boxes In Scene
    private void GetBoxes()
    {
        foreach (BoxScript boxes in FindObjectsOfType<BoxScript>())
        {
            boxList.Add(boxes);
        }
    }

    private void Update()
    {
        GameIsOver();
    }

    //When Game Finished
    private void GameIsOver()
    {
        if (isGameOver) { return; }

        if (IsAllMissionComplete())
        {
            //Raising Event
            GameOver?.Invoke();

            Debug.Log("Game Over");

            isGameOver = true;
        }
    }

    //Checking Are The Balls Inside Of The Box Are Matched
    private bool IsAllMissionComplete()
    {
        for (int i = 0; i < boxList.Count; ++i)
        {
            if (boxList[i].GetIsMatched() == false)
            {
                return false;
            }
        }

        return true;
    }
}