using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{

    public float moveSpeed = 5f; // Velocidad de movimiento del jugador

    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        // Obtener la entrada del jugador en los ejes horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular la dirección de movimiento
        Vector3 movement = new Vector3(-moveHorizontal, 0f, -moveVertical) * moveSpeed * Time.deltaTime;

        // Mover el jugador
        transform.Translate(movement);
    }

}