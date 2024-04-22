using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTest : MonoBehaviour, IObserver
{
    [SerializeField] private SeatRow seatRow;
    [SerializeField] private OrderSO ordersSO;

    private void Start()
    {
        foreach(var npc in GameObject.FindObjectsOfType<NPCController>(true))
        {
            npc.Subscribe(this);
        }
    }

    [ContextMenu("Dar Orden")]
    public void GiveOrder()
    {
        seatRow.ReceiveOrder(ordersSO);
    }

    public void Notify(Actions action)
    {
        Debug.Log(action);
    }
}
