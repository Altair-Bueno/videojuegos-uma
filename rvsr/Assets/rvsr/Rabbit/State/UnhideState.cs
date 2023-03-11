namespace rvsr.Rabbit.State
{
    public class UnhideState : IRabbitState
    {
        public RabbitState rabbitState;

        public UnhideState(RabbitState rabbitState)
        {
            this.rabbitState = rabbitState;
        }

        public void Update()
        {
            if (true)
                // Contacto visual
                rabbitState.state = new ScapeState(rabbitState);
            else
                // Otherwise
                rabbitState.state = new DanceState(rabbitState);
        }
    }
}