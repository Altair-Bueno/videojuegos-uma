using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GirlCharacterMain : MonoBehaviour
{
    // Components
    public NavMeshAgent navMeshAgent;
    public VisionCone visionCone;

    // LayerMasks
    public LayerMask questProviderLayerMask = new LayerMask();
    public LayerMask pickupLayerMask = new LayerMask();

    public Transform[] pointsOfInterest = { };


    void Start()
    {
        StartCoroutine(nameof(GirlCharacterMainCorutine));
    }

    IEnumerator GirlCharacterMainCorutine()
    {
        // Search a quest
        yield return SearchCorutine(questProviderLayerMask);

        // Get near the quest NPC
        var questProvider = visionCone.GetOnSight().First();
        yield return GoToCorutine(navMeshAgent, questProvider.transform.position);
    }

    // Search for something
    IEnumerator SearchCorutine(LayerMask objetive)
    {
        visionCone.layerMask = objetive;
        visionCone.enabled = true;

        // Allow Unity to compute things lmao
        yield return null;
        while (!visionCone.GetOnSight().Any())
        {
            var objective = pointsOfInterest[Random.Range(0, pointsOfInterest.Length)].position;
            yield return GoToCorutine(navMeshAgent, objective);
        }

        visionCone.enabled = false;
    }

    IEnumerator GoToCorutine(NavMeshAgent navMeshAgent, Vector3 objective)
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(objective);
        yield return new WaitUntil(() =>
            {
                var condition = !navMeshAgent.pathPending &&
                    navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance &&
                    (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f);
                return condition;
            }
        );
        navMeshAgent.enabled = false;
    }
}