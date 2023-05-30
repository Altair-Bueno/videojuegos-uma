using UnityEngine;
using UnityEngine.AI;

public class GoToNavMesh : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform destination;

    private void FixedUpdate()
    {
        navMeshAgent.destination = destination.position;
    }
}