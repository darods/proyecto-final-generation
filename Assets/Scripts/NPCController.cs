using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class NPCController : Subject
{
    [Header("NavMeshAgent Variables")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rotationSpeed = 5.0f;

    [Header("Order Management")]
    private OrderManager orderManager;
    private OrderSO[] possibleOrders;
    [SerializeField] private float minTimeToOrder = 5f;
    [SerializeField] private float maxTimeToOrder = 10f;
    private float orderTimer;
    private OrderSO currentOrder;

    [Header("Seat Management")]
    private Seat assignedSeat;

    [Header("Order")]
    [SerializeField] private GameObject orderImage;
    [SerializeField] private float orderTimeout = 15f;
    private bool canMakeOrders = false;

    private void Start()
    {
        orderManager = OrderManager.Instance;
    }

    private void Update()
    {
        if (canMakeOrders && orderTimer > 0)
        {
            orderTimer -= Time.deltaTime;
            if (orderTimer <= 0)
            {
                MakeOrder();
            }
        }

        if (currentOrder != null && orderTimeout > 0)
        {
            orderTimeout -= Time.deltaTime;
            if (orderTimeout <= 0)
            {
                CancelCurrentOrder();
            }
        }
    }

    public void SetTargetPosition(Vector3 destination)
    {
        agent.destination = destination;
        StartCoroutine(CheckAndRotate(destination));
    }

    private IEnumerator CheckAndRotate(Vector3 destination)
    {
        while (!IsAgentAtDestination())
        {
            yield return null;
        }

        Vector3 forwardDirection = (destination - agent.transform.position).normalized;
        forwardDirection.y = 0;

        Quaternion finalRotation = Quaternion.LookRotation(forwardDirection);

        //Smoothly rotate towards the desired rotation
        while (Quaternion.Angle(agent.transform.rotation, finalRotation) > 0.1f)
        {
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, finalRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        agent.transform.rotation = finalRotation;
    }

    public bool IsAgentAtDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude == 0f;
    }

    public void StartMakingOrders()
    {
        canMakeOrders = true;
        ResetOrderTimer();
    }

    private void MakeOrder()
    {
        if (assignedSeat != null && currentOrder == null && assignedSeat.isOccupied)
        {
            possibleOrders = orderManager.GetPossibleOrders();
            if (possibleOrders.Length > 0)
            {
                int orderIndex = Random.Range(0, possibleOrders.Length);
                currentOrder = possibleOrders[orderIndex];

                if (orderManager.TryCreateOrder(currentOrder, assignedSeat))
                {
                    ShowOrderVisual(currentOrder.orderSprite);
                    orderTimeout = 15f;
                }
                else
                {
                    //Debug.Log("Failed to make order, max orders reached");
                    ResetOrderTimer();
                    currentOrder = null;
                }
            }
            else
            {
                //Debug.Log("No possible orders available");
            }
        }
    }

    private void CancelCurrentOrder()
    {
        //Debug.Log("Order time out and was canceled");
        orderManager.OrderCompletedOrCanceled(currentOrder);
        RestoreVisual();
        ResetOrderTimer();
        Notify(Actions.RemovePoints);
        currentOrder = null;
        orderTimeout = 0;
    }

    public bool OnOrderDelivered(OrderSO order)
    {
        if (currentOrder != null && order == currentOrder)
        {
            currentOrder = null;
            orderTimeout = 0;
            RestoreVisual();
            Notify(Actions.AddPoints);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ShowOrderVisual(Texture orderSprite)
    {
        if (orderImage != null)
        {
            orderImage.GetComponent<RawImage>().texture = orderSprite;
            orderImage.SetActive(true);
        }
    }

    private void RestoreVisual()
    {
        if (orderImage != null)
        {
            orderImage.GetComponent<RawImage>().texture = null;
            orderImage.SetActive(false);
        }
    }

    public void SetAssignedSeat(Seat seat)
    {
        assignedSeat = seat;
    }

    private void ResetOrderTimer()
    {
        orderTimer = Random.Range(minTimeToOrder, maxTimeToOrder);
    }
}
