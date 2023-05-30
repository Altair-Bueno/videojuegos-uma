using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GirlCharacterMain : MonoBehaviour
{
    // Components
    public NavMeshAgent navMeshAgent;
    public VisionCone visionCone;
    public TextController textController;
    public ChatGPT chatGpt;

    // LayerMasks
    public LayerMask questProviderLayerMask;
    public LayerMask pickupLayerMask;

    public Transform[] pointsOfInterest = { };

    public GameObject handBone;


    private void Start()
    {
        StartCoroutine(GirlCharacterMainCorutine());
    }

    private IEnumerator GirlCharacterMainCorutine()
    {
        // 1. Search a quest
        print("Step 1: Search a quest");
        yield return SearchCorutine(questProviderLayerMask);

        // 2. Get near the quest NPC
        print("Step 2: Get near the quest NPC");
        var questProviderHit = visionCone.GetOnSight().First();
        yield return GoToCorutine(navMeshAgent, questProviderHit.transform.position);
        var questProvider = questProviderHit.transform.gameObject.GetComponent<QuestProvider>();

        // 3. Init the quest
        print("Step 3: Init the quest");
        yield return questProvider.StartQuest(this);

        // 4. Search for the quest object
        print("Step 4: Search for the quest object");
        do
        {
            yield return SearchCorutine(pickupLayerMask);
        } while (!visionCone.GetOnSight().Any(x => x.transform.CompareTag(questProvider.objetive)));

        // 5. Get nearby the quest object
        print("Step 5: Get nearby the quest object");
        var objetiveRaycastHit = visionCone.GetOnSight().First(x => x.transform.CompareTag(questProvider.objetive));
        yield return GoToCorutine(navMeshAgent, objetiveRaycastHit.transform.position);

        // 6. Pickup the quest object
        print("Step 6: Pickup the quest object");
        var objetiveGameObject = objetiveRaycastHit.transform.gameObject;
        var pickupRotationAnimation = objetiveGameObject.GetComponent<PickupRotationAnimation>();
        var pickupUpdownAnimation = objetiveGameObject.GetComponent<PickupUpdownAnimation>();
        pickupRotationAnimation.enabled = false;
        pickupUpdownAnimation.StopAllCoroutines();
        var oldParent = objetiveGameObject.transform.parent;
        objetiveGameObject.transform.parent = handBone.transform;
        objetiveGameObject.transform.localPosition = Vector3.zero;

        // 7. Return to the quest provider
        print("Step 7: Return to the quest provider");
        yield return GoToCorutine(navMeshAgent, questProvider.transform.position);

        // 8. Drop down the quest object
        print("Step 8: Drop down the quest object");
        objetiveGameObject.transform.parent = oldParent;
        objetiveGameObject.transform.position = transform.position + transform.forward * .25f;
        objetiveGameObject.transform.rotation = Quaternion.identity;
        pickupRotationAnimation.enabled = true;
        pickupUpdownAnimation.StartCoroutine(pickupUpdownAnimation.PickupUpdownAnimationCorutine());

        // 9. End quest
        yield return questProvider.EndQuest(this);

        print("Scene complete");
    }

    // Search for something
    private IEnumerator SearchCorutine(LayerMask objetive)
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

    private IEnumerator GoToCorutine(NavMeshAgent navMeshAgent, Vector3 objective)
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

    private IEnumerator RotateCorutine(Transform transform, float speed, float duration)
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