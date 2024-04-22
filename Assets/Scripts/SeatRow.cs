using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeatRow : MonoBehaviour
{
    [SerializeField] private List<Seat> seats;
    private Queue<Seat> availableSeats;

    public void InitializeSeatsQueue()
    {
        availableSeats = new Queue<Seat>(seats.OrderBy(x => Random.value).ToList());
    }

    public bool HasAvailableSeat()
    {
        return availableSeats.Count > 0;
    }

    public void SetSeat(NPCController npc)
    {
        if (availableSeats.Count > 0)
        {
            Seat seat = availableSeats.Dequeue();
            if (npc != null)
            {
                npc.SetAssignedSeat(seat);
                npc.SetTargetPosition(seat.transform.position);
                seat.AssignNPC(npc);
            }
            else
            {
                //Debug.LogError("Not NPCController given");
            }
        }
        else
        {
            //Debug.LogError("No available seats for NPC");
        }
    }

    public bool ReceiveOrder(OrderSO order)
    {
        foreach (Seat seat in seats)
        {
            if (seat.ReceiveOrder(order))
            {
                Debug.Log("Order delivered to one of the passengers in the row");
                return true;
            }
        }
        Debug.Log("No passenger in this row has the order");
        return false;
    }
}