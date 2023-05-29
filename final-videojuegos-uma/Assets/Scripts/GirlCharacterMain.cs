using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
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

    public GameObject handBone;


    void Start()
    {
        StartCoroutine(GirlCharacterMainCorutine());
    }

    IEnumerator GirlCharacterMainCorutine()
    {
        // Search a quest
        yield return SearchCorutine(questProviderLayerMask);

        // Get near the quest NPC
        var questProviderHit = visionCone.GetOnSight().First();
        yield return GoToCorutine(navMeshAgent, questProviderHit.transform.position);
        var questProvider = questProviderHit.transform.gameObject.GetComponent<QuestProvider>();

        yield return questProvider.StartQuest();

        // Look for the objective
        do
        {
            yield return SearchCorutine(pickupLayerMask);
        } while (!visionCone.GetOnSight().Any(x => x.transform.CompareTag(questProvider.objetive)));

        var objetiveRaycastHit = visionCone.GetOnSight().First(x => x.transform.CompareTag(questProvider.objetive));
        yield return GoToCorutine(navMeshAgent, objetiveRaycastHit.transform.position);
        
        var objetiveGameObject = objetiveRaycastHit.transform.gameObject;
        var pickupRotationAnimation = objetiveGameObject.GetComponent<PickupRotationAnimation>();
        var pickupUpdownAnimation = objetiveGameObject.GetComponent<PickupUpdownAnimation>();
        pickupRotationAnimation.enabled = false;
        pickupUpdownAnimation.StopAllCoroutines();
        var oldParent = objetiveGameObject.transform.parent;
        objetiveGameObject.transform.parent = handBone.transform;
        objetiveGameObject.transform.localPosition = Vector3.zero;

        yield return GoToCorutine(navMeshAgent, questProvider.transform.position);

        objetiveGameObject.transform.parent = oldParent;
        objetiveGameObject.transform.position = this.transform.position + this.transform.forward * .25f;
        objetiveGameObject.transform.rotation = Quaternion.identity;
        pickupRotationAnimation.enabled = true;
        pickupUpdownAnimation.StartCoroutine(pickupUpdownAnimation.PickupUpdownAnimationCorutine());
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
            yield return RotateCorutine(transform, 100f, Random.Range(.3f, .5f));
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

    IEnumerator RotateCorutine(Transform transform, float speed, float duration)
    {
        var time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0f, speed * Time.deltaTime, 0);
            yield return null;
        }
    }
}