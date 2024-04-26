using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

using UnityEngine;
using Unity.Collections;

public class PlayerControllerForMultiplayer : NetworkBehaviour
{
    [Header("Movement Settings")]
    [Space]
    [SerializeField] private float _moveSpeed, _currentSpeed;
    private Rigidbody rb;
    private Vector3 playerMovementInput;
    //test for network variables
    private NetworkVariable<int> score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    //test for more complex netowrk variables
    public struct MyCustomData : INetworkSerializable{
        public int _int;
        public bool _bool;
        public FixedString32Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }

    private NetworkVariable<MyCustomData> randomData = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            _int = 0,
            _bool = true,
            message = "hola",
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
        IsSpacebarPressed();
    }
    private void MovePlayer()
    {

        if(!GameManager.Instance.IsGamePlaying()) return;
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
    public void IsSpacebarPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
            score.Value++;
            randomData.Value = new MyCustomData
            {
                _int = score.Value,
                _bool = false,
                message = "hola1",
            };

            //Debug.Log("OwnerID: "+OwnerClientId+" score: " + score.Value);
            Debug.Log("OwnerID: " + OwnerClientId + " myCustomData int : " + randomData.Value._int + " bool: " + randomData.Value._bool + " msg: " + randomData.Value.message);


            if (IsClient)
            {
                // This will log locally and send the log to the server to be logged there aswell
                //Debug.Log("OwnerID: " + OwnerClientId + " score: " + score.Value);
                Debug.Log("OwnerID: " + OwnerClientId + " myCustomData int : " + randomData.Value._int + " bool: " + randomData.Value._bool + " msg: " + randomData.Value.message);
            }
            */
            //TestServerRpc("message");
            //TestServerRpc(new ServerRpcParams());
            TestClientRpc();
        }
        
    }

    [ServerRpc]
    /*private void TestServerRpc(string msg)
    {
        Debug.Log("TestServerRpc " + OwnerClientId + " msg " + msg);
    }*/
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        Debug.Log("TestServerRpc " + OwnerClientId + "; " + serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void TestClientRpc()
    {
        Debug.Log("test client RPC");
    }
}
