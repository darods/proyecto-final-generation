using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Transform player1SpawnPos;
    [SerializeField] private Transform player2SpawnPos;
    [SerializeField] private CinemachineVirtualCamera singlePlayerCamera;
    [SerializeField] private CinemachineTargetGroup MultiplayerTargetGroup;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("NumberOfPlayers") == 1)
        {
            GameObject player1Instance = Instantiate(player1, player1SpawnPos);
            player1Instance.transform.SetParent(null);
            singlePlayerCamera.Follow = player1Instance.transform;
            singlePlayerCamera.LookAt = player1Instance.transform;

        }
        if (PlayerPrefs.GetInt("NumberOfPlayers") == 2)
        {
            GameObject player1Instance = Instantiate(player1, player1SpawnPos);
            GameObject player2Instance = Instantiate(player2, player2SpawnPos);
            MultiplayerTargetGroup.gameObject.SetActive(true);

            singlePlayerCamera.Follow = MultiplayerTargetGroup.transform;
            singlePlayerCamera.LookAt = MultiplayerTargetGroup.transform;

            MultiplayerTargetGroup.AddMember(player1Instance.transform,1,0);
            MultiplayerTargetGroup.AddMember(player2Instance.transform, 1, 0);



        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
