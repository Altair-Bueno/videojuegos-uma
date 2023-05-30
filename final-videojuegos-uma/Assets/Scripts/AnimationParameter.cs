using UnityEngine;
using UnityEngine.AI;

public class AnimationParameter : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public string animatorParameter = "Speed";

    private int id;

    // Start is called before the first frame update
    private void Start()
    {
        id = Animator.StringToHash(animatorParameter);
    }

    // Update is called once per frame
    private void Update()
    {
        var speed = navMeshAgent.enabled ? navMeshAgent.speed : 0;
        animator.SetFloat(id, speed);
    }
}