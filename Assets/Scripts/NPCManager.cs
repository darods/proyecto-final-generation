using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> NPCList = new List<GameObject>();

    [Header("Spawn Positions")]
    [SerializeField] private Transform leftSpawnPosition;
    [SerializeField] private Transform rightSpawnPosition;

    [Header("Target Positions")]
    [SerializeField] private List<Transform> leftTargetPositions;
    [SerializeField] private List<Transform> rightTargetPositions;

    private Queue<Transform> leftTargetsQueue;
    private Queue<Transform> rightTargetsQueue;

    [Header("Spawning Settings")]
    [SerializeField] private float spawnDelay = 1.0f;

    private int leftNPCsCount = 0;
    private int rightNPCsCount = 0;

    private bool isPlayMode = true;

    private void Start()
    {
        SearchNPCInScene();
        InitializeTargetQueues();
        StartCoroutine(SpawnNPCsEquitably());
    }

    private void SearchNPCInScene()
    {
        var NPCsFound = GameObject.FindObjectsOfType<NPCController>(true);
        //Debug.Log($"NPCs found: {NPCsFound.Length}");
        foreach (var NPC in NPCsFound)
        {
            NPCList.Add(NPC.gameObject);
            //Debug.Log($"Added NPC: {NPC.gameObject.name}");
        }
    }

    private void InitializeTargetQueues()
    {
        leftTargetsQueue = new Queue<Transform>(ShuffleList(leftTargetPositions));
        rightTargetsQueue = new Queue<Transform>(ShuffleList(rightTargetPositions));
    }

    private IEnumerator SpawnNPCsEquitably()
    {
        while (isPlayMode)
        {
            //Decide which side to spawn on based on the current counts
            if (leftNPCsCount <= rightNPCsCount)
            {
                if (leftTargetsQueue.Count > 0)
                {
                    Transform targetPosition = leftTargetsQueue.Dequeue();
                    SpawnNPC(leftSpawnPosition.position, leftSpawnPosition.rotation, targetPosition);
                    leftNPCsCount++;
                }
            }
            else
            {
                if (rightTargetsQueue.Count > 0)
                {
                    Transform targetPosition = rightTargetsQueue.Dequeue();
                    SpawnNPC(rightSpawnPosition.position, rightSpawnPosition.rotation, targetPosition);
                    rightNPCsCount++;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnNPC(Vector3 spawnPosition, Quaternion spawnRotation, Transform targetPosition)
    {
        GameObject npc = ObjectPooler.Instance.SpawnFromPool(ObjectPooler.ObjectsToSpawn.NPC, spawnPosition, spawnRotation);
        npc.GetComponent<NPCController>().SetTargetPosition(targetPosition.position);
    }

    //Utility method to shuffle a list
    private List<T> ShuffleList<T>(List<T> inputList)
    {
        return inputList.OrderBy(x => Random.value).ToList();
    }
}
