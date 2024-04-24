using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour,IInteractable
{
[SerializeField] OrderSO order;
  public GameObject Interact(){
    // posicion, rotacion 
    Debug.Log("entra");
    GameObject spawnedObject = Instantiate(order.prefab, transform.position, transform.rotation);
    return spawnedObject;

  }
}
