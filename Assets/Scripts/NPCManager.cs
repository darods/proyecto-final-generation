using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NPCManager : NetworkBehaviour
{
    [SerializeField] private List<GameObject> NPCList = new List<GameObject>();

    [Header("Spawn Positions")]
    [SerializeField] private Transform leftSpawnPosition;
    [SerializeField] private Transform rightSpawnPosition;
    private bool spawnLeftNext = true;

    [Header("Target Positions")]
    [SerializeField] private List<SeatRow> seatRows;

    [Header("Spawning Settings")]
    [SerializeField] private float spawnDelay = 1.0f;

    private bool isPlayMode = true;
    private bool allNPCsSpawned = false;
    private bool allNPCsSeated = false;

    public float delayTime = 15f; // Adjust this value to set the delay time


    private void Start()
    {
        StartCoroutine(StartDelayed());

        
    }

    IEnumerator StartDelayed()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);
        SearchNPCInScene();
        InitializeTargetQueues();
        StartCoroutine(SpawnNPCsEquitably());

        // Code to execute after the delay
        Debug.Log("Delayed start complete!");
    }

    private void LateUpdate()
    {
        if (allNPCsSpawned && !allNPCsSeated)
        {
            CheckAllNPCsSeated();
        }
    }

    private void SearchNPCInScene()
    {
        var NPCsFound = GameObject.FindObjectsOfType<NPCController>(true);
        foreach (var NPC in NPCsFound)
        {
            NPCList.Add(NPC.gameObject);
        }
    }

    private void InitializeTargetQueues()
    {
        foreach (var seatRow in seatRows)
        {
            seatRow.InitializeSeatsQueue();
        }
    }

    private IEnumerator SpawnNPCsEquitably()
    {
        bool spawnedThisCycle = false;

        while (isPlayMode && !allNPCsSpawned)
        {
            spawnedThisCycle = false;

            foreach (var seatRow in seatRows)
            {
                if (seatRow.HasAvailableSeat())
                {
                    GameObject npc; // spawn forn single player version

                    if (spawnLeftNext)
                    {
                        npc = ObjectPooler.Instance.SpawnFromPool(ObjectPooler.ObjectsToSpawn.NPC, leftSpawnPosition.position, leftSpawnPosition.rotation);
                        npc.GetComponent<NetworkObject>().Spawn(true);
                        spawnLeftNext = false;
                    }
                    else
                    {
                        npc = ObjectPooler.Instance.SpawnFromPool(ObjectPooler.ObjectsToSpawn.NPC, rightSpawnPosition.position, rightSpawnPosition.rotation);
                        npc.GetComponent<NetworkObject>().Spawn(true);
                        spawnLeftNext = true;
                    }
                    seatRow.SetSeat(npc.GetComponent<NPCController>());
                    spawnedThisCycle = true;
                }
                yield return new WaitForSeconds(spawnDelay);
            }

            if (!spawnedThisCycle)
            {
                allNPCsSpawned = true;
            }
        }
    }

    public void CheckAllNPCsSeated()
    {
        allNPCsSeated = true;
        foreach (GameObject npc in NPCList)
        {
            NPCController controller = npc.GetComponent<NPCController>();
            if (controller != null && !controller.IsAgentAtDestination())
            {
                allNPCsSeated = false;
                break;
            }
        }

        if (allNPCsSeated)
        {
            //Debug.Log("All NPCs are seated");
            foreach (GameObject npc in NPCList)
            {
                NPCController controller = npc.GetComponent<NPCController>();
                controller.StartMakingOrders();
            }
        }
    }
}
