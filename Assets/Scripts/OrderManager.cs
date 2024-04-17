using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<OrderSO> foodItems = new List<OrderSO>();
    private Dictionary<int, Order> currentOrders = new Dictionary<int, Order>();

    private bool canOrder = true;

    private void Start()
    {
        StartCoroutine(OrderRoutine());
    }

    private IEnumerator OrderRoutine()
    {
        while (canOrder)
        {
            //Wait for a random time before processing next order
            yield return new WaitForSeconds(Random.Range(5, 10));

            if (currentOrders.Count < 10) //Limit to 10 orders
            {
                ProcessNewOrder();
            }
        }
    }

    private void ProcessNewOrder()
    {
        int npcId = GetRandomNPCId();
        OrderSO order = GetRandomOrder();

        currentOrders.Add(npcId, new Order { OrderItem = order, TimePlaced = Time.time, IsFulfilled = false });

        Debug.Log($"Order placed by NPC {npcId}: {order.itemName}");
    }

    private int GetRandomNPCId()
    {
        return Random.Range(1, 16);
    }

    private OrderSO GetRandomOrder()
    {
        return foodItems[Random.Range(0, foodItems.Count)];
    }

    private void Update()
    {
        var overdueOrders = currentOrders.Where(kvp => !kvp.Value.IsFulfilled && Time.time - kvp.Value.TimePlaced > 30).ToList();

        foreach (var order in overdueOrders)
        {
            IncreaseNPCAnger(order.Key);
            currentOrders.Remove(order.Key);
        }
    }

    private void IncreaseNPCAnger(int npcId)
    {
        Debug.Log($"NPC {npcId} is angry due to late order!");
    }
}

public class Order
{
    public OrderSO OrderItem;
    public float TimePlaced;
    public bool IsFulfilled;
}
