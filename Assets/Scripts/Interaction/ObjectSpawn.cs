using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour,IInteractable
{
[SerializeField] OrderSO order;
  public GameObject Interact(){
    // posicion, rotacion 
    // Instantiate(prefab, position, rotation);
    GameObject spawnedObject = null;
    return spawnedObject;

  }
}
