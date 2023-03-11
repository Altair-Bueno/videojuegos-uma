using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class ScapeState : IRabbitState
    {
        public RabbitState rabbitState;

        public ScapeState(RabbitState rabbitState)
        {
            this.rabbitState = rabbitState;
        }

        public void Update()
        {
            if (Physics.Raycast(rabbitState.transform.position, rabbitState.transform.forward, float.PositiveInfinity,
                    rabbitState.layerMask))
            {
                // Contacto visual
                // NOP
            }
            else if (false)
            {
                // Disparo en el aire
                Destroy();
                rabbitState.state = new HideState(rabbitState);
            }
            else
            {
                // Otherwise
                Destroy();
                rabbitState.state = new DanceState(rabbitState);
            }
        }

        public void Destroy()
        {
        }
    }
}