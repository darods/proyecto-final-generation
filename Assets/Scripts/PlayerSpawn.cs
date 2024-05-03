using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Transform player1SpawnPos;
    [SerializeField] private Transform player2SpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("NumberOfPlayers") == 1)
        {
            Instantiate(player1, player1SpawnPos);
        }
        if (PlayerPrefs.GetInt("NumberOfPlayers") == 2)
        {
            Instantiate(player1, player1SpawnPos);
            Instantiate(player2, player2SpawnPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
