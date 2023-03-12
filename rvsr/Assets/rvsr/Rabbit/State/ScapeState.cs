

// TODO ScapeState

namespace rvsr.Rabbit.State
{
    public class ScapeState : IRabbitState
    {
        public Rabbit rabbit;

        public ScapeState(Rabbit rabbit)
        {
            this.rabbit = rabbit;
        }

        public void Update()
        {
            if (rabbit.RobotOnSight())
            {
                // NOP
            }
            else if (rabbit.MissileNearby())
            {
                rabbit.state.Destroy();
                rabbit.state = new HideState(rabbit);
            }
            else
            {
                rabbit.state.Destroy();
                rabbit.state = new DanceState(rabbit);
            }
        }

        public void Destroy()
        {
        }
    }
}