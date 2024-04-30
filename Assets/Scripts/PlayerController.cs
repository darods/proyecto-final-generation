using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Space]
        [SerializeField] private float _moveSpeed, _currentSpeed, rotationSpeed ;
        private Rigidbody rb;
        private Vector3 playerMovementInput;
        
        [SerializeField] bool isMove = false;
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
        if (isMove){
        MovePlayer();
        PlayerDash();
        }
    }
    private void MovePlayer (){


        playerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        playerMovementInput.Normalize();
        if(playerMovementInput.magnitude > 0.1f){
            Quaternion targetRotation = Quaternion.LookRotation(playerMovementInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        
        Vector3 moveVector = playerMovementInput * _currentSpeed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
            
    }
    private void PlayerDash (){
        if (Input.GetButtonDown("Dash") && !isDashing && playerMovementInput.magnitude > 0.1f) 
        {
            _currentSpeed = _dashSpeed;
            _dashTimer = _dashDuration;
            isDashing = true;
            dashParticles.Play();
            // comenzar el timer del dash 
        }
        
        if (_dashTimer > 0){
            
            _dashTimer -= Time.deltaTime;
            if (_dashTimer <= 0)
            {
                _currentSpeed = _moveSpeed;
                isDashing = false;  // Restaurar la velocidad normal al finalizar el dash
            }
           
        }
    
    }


    public void SetPlayerState (bool state){
        isMove = state;
    }

}
