using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(Actions action)
    {
        foreach (var observer in observers)
        {
            observer.Notify(action);
        }
    }
}