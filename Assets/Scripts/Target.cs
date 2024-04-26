using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    // Start is called before the first frame update
    void Start()
    {   
     targetRb = GetComponent<Rigidbody>();
     
     targetRb.AddForce(RandoForce(), ForceMode.Impulse);
     targetRb.AddTorque(RandoTorque(), RandoTorque(), RandoTorque(), ForceMode.Impulse);
        
    }

    Vector3 RandoForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandoTorque() 
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandoSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
