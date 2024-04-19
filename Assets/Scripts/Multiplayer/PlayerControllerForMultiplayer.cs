using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

using UnityEngine;

public class PlayerControllerForMultiplayer : NetworkBehaviour
{
    [Header("Movement Settings")]
    [Space]
    [SerializeField] private float _moveSpeed, _currentSpeed;
    private Rigidbody rb;
    private Vector3 playerMovementInput;


    [Header("Dash Settings")]
    [Space]
    [SerializeField] private float _dashSpeed = 10.0f; // Velocidad de dash
    [SerializeField] private float _dashDuration = 1f; // Duración del dash en segundos
    private float _dashTimer = 0.0f; // Temporizador para el dash
    private bool isDashing;
    private CharacterController player;
    public ParticleSystem dashParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _currentSpeed = _moveSpeed;
        isDashing = false;
    }
    void Update()
    {

        if (!IsOwner)
        {
            return;
        }
        MovePlayer();
        PlayerDash();

    }
    private void MovePlayer()
    {
        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        playerMovementInput.Normalize();
        transform.LookAt(transform.position + playerMovementInput);

        // Vector3 moveVector = transform.TransformDirection(playerMovementInput) * _currentSpeed;
        Vector3 moveVector = playerMovementInput * _currentSpeed;

        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }
    private void PlayerDash()
    {
        if (Input.GetButtonDown("Dash") && !isDashing && playerMovementInput.magnitude > 0.1f)
        {
            _currentSpeed = _dashSpeed;
            _dashTimer = _dashDuration;
            isDashing = true;
            dashParticles.Play();
            // comenzar el timer del dash 
        }

        if (_dashTimer > 0)
        {

            _dashTimer -= Time.deltaTime;
            if (_dashTimer <= 0)
            {
                _currentSpeed = _moveSpeed;
                isDashing = false;  // Restaurar la velocidad normal al finalizar el dash
            }

        }

    }
}