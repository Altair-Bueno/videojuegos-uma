using UnityEngine;

// TODO HideState

namespace rvsr.Rabbit.State
{
    public class HideState : IRabbitState
    {
        public Rabbit rabbit;

        private float timer;

        public HideState(Rabbit rabbit)
        {
            this.rabbit = rabbit;
            timer = Random.Range(this.rabbit.hideMinDuration, this.rabbit.hideMaxDuration);
            // TODO hide rabbit
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Destroy();
                rabbit.state = new UnhideState(rabbit);
            }
        }

        public void Destroy()
        {
            // TODO unhide rabbit
        }

        public void OnCollision(Collision collision)
        {
            // TODO hit collision
            if (false)
            {
                Destroy();
                rabbit.state = new UnhideState(rabbit);
            }
        }
    }
}