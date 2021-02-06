using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static event Action IsGameOver;

    private List<ReplacementController> replacementLists = new List<ReplacementController>();

    private bool isGameOver;

    private void Start()
    {
        GetReplacements();
    }

    //Get Replacements In Scene
    private void GetReplacements()
    {
        foreach (ReplacementController replacements in FindObjectsOfType<ReplacementController>())
        {
            replacementLists.Add(replacements);
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
            IsGameOver?.Invoke();

            Debug.Log("Game Over");

            isGameOver = true;
        }
    }

    //Checking Are The Box Inside Of The Replacements Are Matched
    private bool IsAllMissionComplete()
    {
        for (int i = 0; i < replacementLists.Count; ++i)
        {
            if (replacementLists[i].GetIsMatched() == false)
            {
                return false;
            }
        }

        return true;
    }
}
