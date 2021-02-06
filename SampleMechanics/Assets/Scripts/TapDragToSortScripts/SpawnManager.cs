using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<GameObject> spawnedObjects = new List<GameObject>();
    [SerializeField] private GameObject[] boxPrefab;
    [SerializeField] private GameObject[] spawnLocation;

    private void Start()
    {
        InstantiateRandomBox();
    }

    private void InstantiateRandomBox()
    {
        GetLocations();

        CreateBox();

        ShuffleList();

        PlaceBoxes();
    }

    //Get Scene Location Positions
    private void GetLocations()
    {
        spawnLocation = GameObject.FindGameObjectsWithTag("Location");
    }

    //Instantiating The Same Number Of Each Box
    private void CreateBox()
    {
        foreach (GameObject box in boxPrefab)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject InstantiateBox = Instantiate(box, new Vector3(0f, 0f, 0f), Quaternion.identity);
                //Add Instantiated Box To The List
                spawnedObjects.Add(InstantiateBox);
            }

        }
    }

    //Shuffle List Elements For ReOrder Them
    private void ShuffleList()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            GameObject temp = spawnedObjects[i];
            int randomIndex = Random.Range(i, spawnedObjects.Count);
            spawnedObjects[i] = spawnedObjects[randomIndex];
            spawnedObjects[randomIndex] = temp;
        }
    }

    //Place Box To The Locations
    private void PlaceBoxes()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            spawnedObjects[i].transform.position = spawnLocation[i].transform.position;
        }
    }
}
