using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{    GameObject Interact();
}

public class GetObject : MonoBehaviour
{
       
[Header("Interact Settings")]
[Space]
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask layerObject;
    [SerializeField] private LayerMask layerSpawn;
    private int pickUpLayer, objectsLayer;
    private Rigidbody rbObject;
    private GameObject pickedObject = null;
    private Vector3 offset;

[Header("Launch Settings")]
    [SerializeField] private float launchForce = 4f;
    [SerializeField] private float _maxLaunchTime = 4f;
    private float _launchTimer = 0f;

    private bool isHolding = false;

    private void Start() {
        pickUpLayer = LayerMask.NameToLayer("PickUpLayer");
        objectsLayer = LayerMask.NameToLayer("Objects");
        offset = new Vector3 (0.00300000003f,-0.125f ,1.01800001f);
    }
    void Update()
    {
        CheckPressed();
        CheckForInteractions();
       
    }

    private void CheckForInteractions (){
              
        if (Input.GetButtonDown("TakeObject") ) {
            if (pickedObject == null){
                RaycastHit _hit;
                if (Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out _hit, transform.rotation, interactRange))
                    {
                        GameObject gameObjectColision =  _hit.collider.gameObject;
                        if (gameObjectColision.layer == layerObject)
                        {
                            PickUpObject(_hit);
                        }
                        else if (gameObjectColision.layer == layerSpawn)
                        {
                            
                           if (gameObjectColision.TryGetComponent(out IInteractable interactObj)){
                                GameObject spawn = interactObj.Interact();  
                           }
                                
                        }
                    }
                                        } else {
                isHolding = true;
            }
        }    

            
    
    
        

    if(Input.GetButtonUp("TakeObject") && pickedObject != null && isHolding){
       
            Lanzar();
            _launchTimer = 0.0f;
            isHolding = false;
    }
    
}
    private void PickUpObject (RaycastHit _hit){
            
            rbObject = _hit.collider.gameObject.GetComponent<Rigidbody>();
            
           
            if (rbObject != null){
                rbObject.useGravity = false;
                rbObject.isKinematic = true;
                pickedObject = _hit.collider.gameObject;
                pickedObject.transform.SetParent(gameObject.transform);
                pickedObject.layer = pickUpLayer;
                pickedObject.transform.localPosition = offset;
            }
            
        }
    private void Lanzar()
    {
        // Dirección de lanzamiento diagonal (hacia arriba y adelante)
        Vector3 direccionLanzamiento = transform.up + transform.forward;

        // Normalizar la dirección de lanzamiento para mantener la magnitud de la fuerza
        direccionLanzamiento.Normalize();
        rbObject.useGravity = true;
        rbObject.isKinematic = false;  
        rbObject.AddForce(direccionLanzamiento * (launchForce * _launchTimer / _maxLaunchTime), ForceMode.Impulse);  
    
        pickedObject.transform.SetParent(null);
        pickedObject.layer = objectsLayer;
        pickedObject = null;
        rbObject = null;
 
    }
    private void CheckPressed()
    {
        if (Input.GetButton("TakeObject") && pickedObject != null)
            _launchTimer += Time.deltaTime;
        if (_launchTimer > _maxLaunchTime)
            _launchTimer = _maxLaunchTime;  
    }


    
}
