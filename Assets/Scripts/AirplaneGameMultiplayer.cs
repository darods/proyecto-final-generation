using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AirplaneGameMultiplayer : NetworkBehaviour
{
    // Start is called before the first frame update
    public static AirplaneGameMultiplayer Instance { get; private set; }

    public void StartHost() 
    {
        Debug.Log("HOST");
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient() 
    {
        Debug.Log("CLIENT");
        NetworkManager.Singleton.StartClient();
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
