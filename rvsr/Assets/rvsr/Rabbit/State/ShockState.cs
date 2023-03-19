using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class ShockState : IRabbitState
    {
        public Rabbit rabbit;

        private float timer;

        public ShockState(Rabbit rabbit)
        {
            Debug.Log("Rabbit Shock");

            this.rabbit = rabbit;
            timer = Random.Range(rabbit.shockMinDuration, rabbit.shockMaxDuration);
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if (timer > 0)
            {
                // NOP
            }

            if (rabbit.RobotNearby())
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