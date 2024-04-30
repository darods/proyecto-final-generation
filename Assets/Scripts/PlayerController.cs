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

        private Animator anim;
        private float movementX;
        private float movementY;
        
        [SerializeField] bool isMove = true;
    [Header("Dash Settings")]
    [Space]
        [SerializeField] private float _dashSpeed = 10.0f; // Velocidad de dash
        [SerializeField] private float _dashDuration = 1f; // Duración del dash en segundos
        private float _dashTimer = 0.0f; // Temporizador para el dash
        private bool isDashing;
        private CharacterController player;
        public ParticleSystem dashParticles;

    [Header("Input Settings")]
    [Space]
        [SerializeField] private string inputNameHorizontal;
        [SerializeField] private string inputNameVertical;
        [SerializeField] private string dashName;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _currentSpeed = _moveSpeed;
        isDashing = false;
        movementX = 0;
        movementY = 0;
}
    void Update()
    {
        if (isMove){
            Debug.Log("hola mundoo");
            MovePlayer();
            PlayerDash();
        }
    }
    private void MovePlayer (){


        playerMovementInput = GetPlayerInput();
        playerMovementInput.Normalize();
        if(playerMovementInput.magnitude > 0.1f){
            Quaternion targetRotation = Quaternion.LookRotation(playerMovementInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        
        Vector3 moveVector = playerMovementInput * _currentSpeed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
            
    }
    private void PlayerDash (){
        if (Input.GetButtonDown(dashName) && !isDashing && playerMovementInput.magnitude > 0.1f) 
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

    Vector3 GetPlayerInput()
    {

        //Vector3 vel = new Vector3(movementX * Time.deltaTime, 0, movementY * Time.deltaTime);
        //Debug.Log(vel);
        return new Vector3(Input.GetAxisRaw(inputNameHorizontal), 0f, Input.GetAxisRaw(inputNameVertical));
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
            movementY = 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            movementY = -1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            movementX = -1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            movementX = 1;

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            movementY = 0;

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            movementX = 0;
            
        return vel;
        */
    }
    public void SetPlayerState (bool state){
        isMove = state;
    }

}
