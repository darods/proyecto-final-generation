using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rotationSpeed = 5.0f;

    public void SetTargetPosition(Vector3 destination)
    {
        agent.destination = destination;
        StartCoroutine(CheckAndRotate(destination));
    }

    private IEnumerator CheckAndRotate(Vector3 destination)
    {
        while (!IsAgentAtDestination())
        {
            yield return null;
        }

        //Calculate the forward vector based on the last segment of the agent's path
        Vector3 forwardDirection = (destination - agent.transform.position).normalized;
        forwardDirection.y = 0;

        Quaternion finalRotation = Quaternion.LookRotation(forwardDirection);

        //Smoothly rotate towards the desired rotation
        while (Quaternion.Angle(agent.transform.rotation, finalRotation) > 0.1f)
        {
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, finalRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        //Ensure the rotation is exactly the final rotation after finishing the interpolation
        agent.transform.rotation = finalRotation;
    }

    private bool IsAgentAtDestination()
    {
        //Check if the agent is within a small distance of the destination and has stopped
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.sqrMagnitude == 0f;
    }
}
