using rvsr.Rabbit.State;
using UnityEngine;

public class RabbitState : MonoBehaviour
{
    public Animation animation;
    public LayerMask layerMask;
    public IRabbitState state;


    // Start is called before the first frame update
    private void Start()
    {
        state = new DanceState(this);
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    private void Update()
    {
        state.Update();
    }
}