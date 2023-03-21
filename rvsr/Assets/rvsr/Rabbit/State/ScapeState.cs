
using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class ScapeState : IRabbitState
    {
        public Rabbit rabbit;

        public ScapeState(Rabbit rabbit)
        {
            Debug.Log("Rabbit Scape");

            this.rabbit = rabbit;
        }

        public void Update()
        {
            var nearbyRobots = rabbit.NearbyRobots();
            if (nearbyRobots.Length > 0)
            {
                var robot = nearbyRobots[0];
                var difference = rabbit.transform.position - robot.transform.position;
                rabbit.rigidbody.MovePosition(rabbit.transform.position + this.rabbit.movementSpeed * Time.deltaTime * difference.normalized);
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

        public void OnCollision(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("HitSphere"))
            {
                this.rabbit.state.Destroy();
                rabbit.state = new ShockState(rabbit);
            }
        }

        public void Destroy()
        {
        }
    }
}