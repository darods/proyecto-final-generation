using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToSpawn; // Reference to the object prefab to be spawned
    public Vector3 spawnPosition;    // Position where the object will be spawned

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObject()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
    //public static AirplaneObject SpawnAirplaneObject(AirplaneObject airplaneObjectSO, IAirplaneObjectParent airplaneObjectParent)
    //{
    //    Transform 
    //}
}
