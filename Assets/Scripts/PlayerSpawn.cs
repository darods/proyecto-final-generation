using Cinemachine;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private GameObject player1Instance;
    [SerializeField] private GameObject player2Instance;

    [SerializeField] private Transform player1SpawnPos;
    [SerializeField] private Transform player2SpawnPos;

    [SerializeField] private CinemachineVirtualCamera singlePlayerCamera;
    [SerializeField] private CinemachineTargetGroup MultiplayerTargetGroup;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("NumberOfPlayers") == 1)
        {
            player1Instance = Instantiate(player1Prefab, player1SpawnPos);
            player1Instance.transform.SetParent(null);
            singlePlayerCamera.Follow = player1Instance.transform;
            singlePlayerCamera.LookAt = player1Instance.transform;

        }
        if (PlayerPrefs.GetInt("NumberOfPlayers") == 2)
        {
            player1Instance = Instantiate(player1Prefab, player1SpawnPos);
            player2Instance = Instantiate(player2Prefab, player2SpawnPos);
            player1Instance.transform.SetParent(null);
            player2Instance.transform.SetParent(null);
            MultiplayerTargetGroup.gameObject.SetActive(true);

            singlePlayerCamera.Follow = MultiplayerTargetGroup.transform;
            singlePlayerCamera.LookAt = MultiplayerTargetGroup.transform;

            MultiplayerTargetGroup.AddMember(player1Instance.transform,1,0);
            MultiplayerTargetGroup.AddMember(player2Instance.transform, 1, 0);
        }
    }

    public PlayerController ReturnPlayer1Spawned()
    {
        if (player1Instance != null)
        {
            return player1Instance.GetComponent<PlayerController>();
        }
        return null;
    }

    public PlayerController ReturnPlayer2Spawned()
    {
        if(player2Instance != null)
        {
            return player2Instance.GetComponent<PlayerController>();
        }
        return null;
    }
}
