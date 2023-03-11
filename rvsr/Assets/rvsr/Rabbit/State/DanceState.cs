using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class DanceState : IRabbitState
    {
        public RabbitState rabbitState;

        public DanceState(RabbitState rabbitState)
        {
            this.rabbitState = rabbitState;
            this.rabbitState.animation.enabled = true;
        }

        public void Update()
        {
            if (Physics.Raycast(rabbitState.transform.position, rabbitState.transform.forward, float.PositiveInfinity,
                    rabbitState.layerMask))
            {
                // Contacto visual
                Destroy();
                rabbitState.state = new ScapeState(rabbitState);
            }
        }

        public void Destroy()
        {
            rabbitState.animation.enabled = false;
        }
    }
}