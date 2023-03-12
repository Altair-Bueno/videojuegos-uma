namespace rvsr.Rabbit.State
{
    public class DanceState : IRabbitState
    {
        public Rabbit rabbit;

        public DanceState(Rabbit rabbit)
        {
            this.rabbit = rabbit;
            this.rabbit.animation.enabled = true;
        }

        public void Update()
        {
            if (rabbit.RobotOnSight())
            {
                Destroy();
                rabbit.state = new ScapeState(rabbit);
            }
        }

        public void Destroy()
        {
            rabbit.animation.enabled = false;
        }
    }
}