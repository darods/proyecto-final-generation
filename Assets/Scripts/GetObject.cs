using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    GameObject Interact();
}

public class GetObject : MonoBehaviour
{

    [Header("Interact Settings")]
    [Space]
    [SerializeField] private float interactRange = 2f;
    [SerializeField] LayerMask layerObject;
    private int pickUpLayer, objectsLayer;
    private Rigidbody rbObject;
    private GameObject pickedObject = null;
    private Vector3 offset;

    [Header("Launch Settings")]
    [SerializeField] private float launchForce = 4f;
    [SerializeField] private float _maxLaunchTime = 4f;
    private float _launchTimer = 0f;

    private bool isHolding = false;

    private void Start()
    {
        pickUpLayer = LayerMask.NameToLayer("PickUpLayer");
        objectsLayer = LayerMask.NameToLayer("Objects");
        offset = new Vector3(0.00300000003f, -0.125f, 1.01800001f);
    }
    void Update()
    {
        CheckPressed();
        CheckForInteractions();

    }

    private void CheckForInteractions()
    {
        //TODO: CORREGIR SOLTAR LANZAR COGER Y ENTREGAR
    

        if (Input.GetButtonDown("TakeObject"))
        {
            RaycastHit _hit;
            if (Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out _hit, transform.rotation, interactRange, layerObject))
            {
                GameObject gameObjectColision = _hit.collider.gameObject;
                if (pickedObject == null)
                {


                    if (gameObjectColision.TryGetComponent(out IInteractable interactObj))
                    {
                        GameObject spawn = interactObj.Interact();
                        PickUpObject(spawn);
                         
                        return;
                    }

                    PickUpObject(_hit.collider.gameObject);
                    
                }
                else {
                     if (gameObjectColision.TryGetComponent(out SeatRow interactObj)){
                        Order order = pickedObject.GetComponent<Order>();
                        bool receiveOrder = interactObj.ReceiveOrder(order.order);
                        pickedObject.SetActive(!receiveOrder);
                        Lanzar();
                        isHolding = !receiveOrder;
                        
                     }


                }
            }
            
            
            
           
        }

  
    if (Input.GetButtonDown("TakeObject") && pickedObject != null && isHolding)
        {
            
            Lanzar();
            _launchTimer = 0.0f;
            isHolding = false;
        }


    }


    

    private void PickUpObject(GameObject obj)
    {
        Debug.Log(obj);
        rbObject = obj.GetComponent<Rigidbody>();


        if (rbObject != null)
        {
            rbObject.useGravity = false;
            rbObject.isKinematic = true;
            pickedObject = obj;
            pickedObject.transform.SetParent(gameObject.transform);
            pickedObject.layer = pickUpLayer;
            pickedObject.transform.localPosition = offset;
        }

    }
    private void Lanzar()
    {
        Vector3 direccionLanzamiento = transform.up + transform.forward;
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
