using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToNavMesh : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform destination;
    
    void FixedUpdate()
    {
        navMeshAgent.destination = destination.position;
    }
}
