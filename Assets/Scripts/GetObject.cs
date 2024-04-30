using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public interface IInteractable
{
    GameObject Interact();
}

public class GetObject : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private PlayerController player; 

    [Header("Interact Settings")]
    [Space]
    [SerializeField] private float interactRange = 2f;
    [SerializeField] LayerMask layerObject;
    private int pickUpLayer, objectsLayer;
    private Rigidbody rbObject;
    private GameObject pickedObject = null;
    private Vector3 offset;
    [SerializeField] private string interactName;

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
        QuickTime ();

    }

    private void CheckForInteractions()
    {
        Debug.Log("intectact state: " + Input.GetButtonDown(interactName));
        //TODO: CORREGIR SOLTAR LANZAR COGER Y ENTREGAR
        if (Input.GetButtonDown(interactName))
        {
            RaycastHit _hit;
            if (Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out _hit, transform.rotation, interactRange, layerObject))
            {
                GameObject gameObjectColision = _hit.collider.gameObject;

                if (!pickedObject)
                {
                    
                     if (gameObjectColision.TryGetComponent(out Pilot pilot))
                    {
                        QuickTime();
                        // Order order = pickedObject.GetComponent<Order>();
                        bool quicktimeEvent = true;
                        if (quicktimeEvent){
                            pilot.WakeUp();
                        }
                        
                    }else{
                        GameObject Object;
                        if (gameObjectColision.TryGetComponent(out IInteractable interactObj))
                        {
                            // COGER SPAWN
                            Debug.Log("coger spawn");
                            Object = interactObj.Interact();
                        }
                        else
                        {
                            // COGER PISO
                            Debug.Log("coger piso");
                            Object = _hit.collider.gameObject;
                        }
                        PickUpObject(Object);
                    }

                    
                    

                }
                else
                {
                    if (gameObjectColision.TryGetComponent(out SeatRow interactObj))
                    {
                        Order order = pickedObject.GetComponent<Order>();
                        bool receiveOrder = interactObj.ReceiveOrder(order.order);
                        pickedObject.SetActive(false);
                        Lanzar();
                    }
                    
                }
                return;

            }
            isHolding = true;

        }
        if (Input.GetButtonUp(interactName) && pickedObject && isHolding)
        {
            
            //LANZAR
            Debug.Log("Lanzar");
            Lanzar();
            isHolding = false;
            _launchTimer = 0.0f;
        }

 

    }
    private void PickUpObject(GameObject obj)
    {
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
        if (Input.GetButton(interactName) && pickedObject != null)
            _launchTimer += Time.deltaTime;
        if (_launchTimer > _maxLaunchTime)
            _launchTimer = _maxLaunchTime;
    }




public KeyCode key1; // Definir las teclas desde el Inspector
public KeyCode key2;

private bool pressKey1 = false; // Indicar si se ha presionado la primera tecla
private bool pressKey2 = true   ; // Indicar si se ha presionado la segunda tecla
private int complete = 0; // Contador de éxitos

public void QuickTime()
{
    
    
    if (Input.GetKeyDown(key1) && pressKey2) 
    {
        pressKey1 = true;
        pressKey2 = false;
        complete += 10;
    }
    else if (Input.GetKeyDown(key2) && pressKey1) 
    {
        pressKey1 = false;
        pressKey2 = true;
        complete += 10 ;
    }

    Debug.Log("Puntos: " + complete);
}





}
