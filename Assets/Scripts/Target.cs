using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float maxTorque = 10f;

    private void Awake()
    {
        // Get the Rigidbody component attached to this GameObject
        targetRb = GetComponent<Rigidbody>();
    }

    // Public method to activate the object and apply forces
    public void ActivateObject()
    {
        AddForceToObject();
    }

    private Vector3 RandoForce()
    {
        // Generate a random upward force based on min and max speeds
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandoTorque()
    {
        // Generate a random torque value within the specified range
        return Random.Range(-maxTorque, maxTorque);
    }

    private void AddForceToObject()
    {
        // Apply random upward force and random torque in all three axes
        targetRb.AddForce(RandoForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandoTorque(), RandoTorque(), RandoTorque(), ForceMode.Impulse);
    }
}
