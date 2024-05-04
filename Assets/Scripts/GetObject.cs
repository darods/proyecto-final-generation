using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Vector3 offset;
    ButtonMashing buttonMash;
    [SerializeField] private string interactName;

    [Header("Launch Settings")]
    [SerializeField] private float launchForce = 4f;
    [SerializeField] private float _maxLaunchTime = 4f;
    private float _launchTimer = 0f;
    private bool isHolding = false;
    [SerializeField ] private float maxDelayTimer = 2f; 
    private float currentDelayTimer; 

    public delegate void GameEndCallback(bool isWin);
    public event GameEndCallback OnGameEnd;

    private Pilot pilotObj ;
    private void Start()
    {
        buttonMash = GetComponentInParent<ButtonMashing>();
        pickUpLayer = LayerMask.NameToLayer("PickUpLayer");
        objectsLayer = LayerMask.NameToLayer("Objects");
        offset = new Vector3(0f ,-0.00899999961f ,0.536000013f );
        currentDelayTimer = maxDelayTimer;
        
    }

    void Update()
    {
        CheckPressed();
        CheckForInteractions();
    }

    private void CheckForInteractions()
    {
        if (Input.GetButtonDown(interactName))
        {
            RaycastHit _hit;
            Transform parent = transform.parent;
            
            if (Physics.BoxCast(transform.position, parent.localScale /2 , transform.forward, out _hit, parent.rotation, interactRange, layerObject))
            {
                GameObject gameObjectColision = _hit.collider.gameObject;

                if (!pickedObject)
                {
                    if (gameObjectColision.TryGetComponent(out Pilot pilot))
                    {
                        pilotObj = pilot;
                        if (pilot.IsAsleep())
                        {
                            
                            buttonMash.StartGame();
                            buttonMash.OnGameEnd += HandleGameEnd;
                        }
                    }
                    else
                    {
                        GameObject Object;
                        if (gameObjectColision.TryGetComponent(out IInteractable interactObj))
                        {
                            Debug.Log("coger spawn");
                            Object = interactObj.Interact();
                        }
                        else
                        {
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

         if (currentDelayTimer > 0f)
        {
            currentDelayTimer -= Time.deltaTime;
        }


        if (Input.GetButtonUp(interactName) && pickedObject && isHolding && currentDelayTimer <= 0)
        {
            Debug.Log("Lanzar");
            Lanzar();
            isHolding = false;
            _launchTimer = 0.0f;
            currentDelayTimer = maxDelayTimer;
        }
    }

    private void PickUpObject(GameObject obj)
    {
            rbObject = obj.GetComponent<Rigidbody>();
        if (rbObject != null)
        {
            Debug.Log("aqui almenos llega");
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

    void HandleGameEnd(bool isWin)
    {
        buttonMash.OnGameEnd -= HandleGameEnd;
        if (isWin)
        {
            pilotObj.WakeUp();
            Debug.Log("El jugador ganó");
        }
      
    }


     private void OnDrawGizmos()
    {
        RaycastHit hit;
        Transform parent = transform.parent;

        if (Physics.BoxCast(transform.position, parent.localScale/2 , transform.forward, out hit, parent.rotation, interactRange))
        {
            // Se ha golpeado algo, puedes usar 'hit' para obtener información sobre el golpe.
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * interactRange);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, parent.localScale);
        }
        else
        {
            // No se ha golpeado nada, dibujar solo el rayo
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * interactRange);
        }
    }
}
