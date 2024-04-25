using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour,IInteractable
{
[SerializeField] ObjectPooler.ObjectsToSpawn typeOfObject;

  public GameObject Interact(){
    
    GameObject spawnedObject = ObjectPooler.Instance.SpawnFromPool(typeOfObject, transform.position, transform.rotation);
    return spawnedObject;

  }
}
