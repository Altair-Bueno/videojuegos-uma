using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class UnhideState : IRabbitState
    {
        public Rabbit rabbit;

        private float timer;

        public UnhideState(Rabbit rabbit)
        {
            this.rabbit = rabbit;
            timer = Random.Range(rabbit.unHideMinDuration, rabbit.unHideMaxDuration);
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if (timer > 0)
            {
                // NOP
            }

            if (rabbit.RobotOnSight())
            {
                rabbit.state.Destroy();
                rabbit.state = new ScapeState(rabbit);
            }
            else
            {
                rabbit.state.Destroy();
                rabbit.state = new DanceState(rabbit);
            }
        }
    }
}