using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderTest : MonoBehaviour, IObserver
{
    [SerializeField] private SeatRow seatRow;
    [SerializeField] private OrderSO ordersSO;
    [SerializeField] private Pilot pilot;

    private void Start()
    {
        foreach(var npc in GameObject.FindObjectsOfType<NPCController>(true))
        {
            npc.Subscribe(this);
        }

        pilot = GameObject.FindAnyObjectByType<Pilot>();
        pilot.Subscribe(this);
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

    [ContextMenu("Iniciar Piloto")]
    public void StartPilotTimer()
    {
        pilot.StartSleepDelay();
    }

    [ContextMenu("Despertar Piloto")]
    public void WakeUpPilot()
    {
        pilot.WakeUp();
    }
}
