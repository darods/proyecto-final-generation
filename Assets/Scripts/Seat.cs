using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool isOccupied { get; private set; }
    public OrderSO currentOrder { get; private set; }
    public NPCController passenger { get; private set; }

    public void AssignNPC(NPCController npc)
    {
        isOccupied = true;
        passenger = npc;
    }

    public bool ReceiveOrder(OrderSO order)
    {
        if (isOccupied)
        {
            return passenger.OnOrderDelivered(order); ;
        }
        //Debug.Log("No passanger in the seat");
        return false;
    }
}
