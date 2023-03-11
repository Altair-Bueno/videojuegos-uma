namespace rvsr.Rabbit.State
{
    public class HideState : IRabbitState
    {
        public RabbitState rabbitState;

        public HideState(RabbitState rabbitState)
        {
            this.rabbitState = rabbitState;
        }

        public void Update()
        {
            if (true || true)
            {
                // Timeout/golpe
                Destroy();
                rabbitState.state = new UnhideState(rabbitState);
            }
        }

        public void Destroy()
        {
        }
    }
}