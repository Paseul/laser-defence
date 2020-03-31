using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgentsExamples;

public class simArea : Area
{
    public GameObject building;
    public GameObject car;
    public GameObject[] spawnAreas;
    public int numObstacle;
    public float range;

    public void CreateBuilding(int numObjects, int spawnAreaIndex)
    {
        CreateObject(numObjects, building, spawnAreaIndex);
    }
    
    public void CreateCar(int numObjects, int spawnAreaIndex)
    {
        CreateObject(numObjects, car, spawnAreaIndex);
    }

    private void CreateObject(int numObjects, GameObject desiredObject, int spawnAreaIndex)
    {
        for (var i = 0; i < numObjects; i++)
        {
            var newObject = Instantiate(desiredObject, Vector3.zero, 
                Quaternion.Euler(0f, 0f, 0f), transform);
            PlaceObject(newObject, spawnAreaIndex);
        }
    }

    public void PlaceObject(GameObject objectToPlace, int spawnAreaIndex)
    {
        var spawnTransform = spawnAreas[spawnAreaIndex].transform;
        var xRange = spawnTransform.localScale.x / 2.1f;
        var zRange = spawnTransform.localScale.z / 2.1f;
        
        objectToPlace.transform.position = new Vector3(Random.Range(-xRange, xRange), 1f, Random.Range(-zRange, zRange)) 
                                            + spawnTransform.position;
        if(objectToPlace.CompareTag("building"))
        {
            objectToPlace.transform.position += new Vector3(0f, 0f, 0f);
        }
        if (objectToPlace.CompareTag("car"))
        {
            objectToPlace.transform.position += new Vector3(0f, 0f, 0f);
        }
        else if (objectToPlace.CompareTag("agent"))
        {
            objectToPlace.transform.position += new Vector3(0f, 4f, 0f);
        }
    }

    public void CleanSimArea()
    {
        foreach (Transform child in transform) if (child.CompareTag("building") || child.CompareTag("car"))
        {
            Destroy(child.gameObject);
        }
    }

    public override void ResetArea()
    {
        
    }
}   
