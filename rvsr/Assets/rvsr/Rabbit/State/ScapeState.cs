using UnityEngine;
using UnityEngine.AI;

namespace rvsr.Rabbit.State
{
    public class ScapeState : IRabbitState
    {
        public Rabbit rabbit;

        private bool canDodge;

        public ScapeState(Rabbit rabbit)
        {
            Debug.Log("Rabbit Scape");

            this.rabbit = rabbit;

            canDodge = Random.Range(0, 100) <= this.rabbit.dodgeLuck;

            this.rabbit.navMeshAgent.enabled = true;
            var destinationCampfire = rabbit.campFires[Random.Range(0, rabbit.campFires.Length)];
            this.rabbit.navMeshAgent.SetDestination(destinationCampfire.transform.position);
        }

        public void Update()
        {
            var missileNearby = rabbit.MissileNearby();
            if (canDodge && missileNearby)
            {
                rabbit.state.Destroy();
                rabbit.state = new DodgeState(rabbit);
            }
            else if (missileNearby)
            {
                // NOP
            }
            else if (rabbit.NearbyRobots().Length == 0 &&
                     rabbit.navMeshAgent.remainingDistance <= rabbit.navMeshAgent.stoppingDistance)
            {
                rabbit.state.Destroy();
                rabbit.state = new DanceState(rabbit);
            }
        }

        public void OnCollision(Collision collision)
        {
            var hitSphereLayer = LayerMask.NameToLayer("HitSphere");
            var missileLayer = LayerMask.NameToLayer("Missile");

            if (collision.gameObject.layer == hitSphereLayer || collision.gameObject.layer == missileLayer)
            {
                this.rabbit.state.Destroy();
                rabbit.state = new ShockState(rabbit);
            }
        }

        public void Destroy()
        {
            rabbit.navMeshAgent.isStopped = true;
            rabbit.navMeshAgent.enabled = false;
        }
    }
}