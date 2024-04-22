using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    [SerializeField] private int maxActiveOrders = 5;
    private List<OrderSO> activeOrders = new List<OrderSO>();

    [SerializeField] private OrderSO[] possibleOrders;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanMakeOrder()
    {
        return activeOrders.Count < maxActiveOrders;
    }

    public bool TryCreateOrder(OrderSO order, Seat seat)
    {
        if (CanMakeOrder())
        {
            if (order == null)
            {
                order = possibleOrders[Random.Range(0, possibleOrders.Length)];
            }

            activeOrders.Add(order);

            return true;
        }
        return false;
    }

    public void OrderCompletedOrCanceled(OrderSO order)
    {
        activeOrders.Remove(order);
    }

    public OrderSO[] GetPossibleOrders()
    {
        return possibleOrders;
    }
}