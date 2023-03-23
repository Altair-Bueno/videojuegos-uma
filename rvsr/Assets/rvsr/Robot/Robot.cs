using System;
using System.Linq;
using rvsr.Robot.State;
using rvsr.Robot.State.Hit;
using rvsr.Robot.State.Patrol;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace rvsr.Robot
{
    public class Robot : MonoBehaviour
    {
        public IRobotState state;

        // Components
        public Rigidbody rigidbody;
        public Renderer renderer;
        public NavMeshAgent navMeshAgent;
        public GameObject nose;

        // General variables
        public LayerMask rabbitLayerMask;
        public LayerMask rabbitNoisesLayerMask;
        public float movementSpeed = 10;
        public float hearRadious = 10;

        // Attack state
        public Missile missile;
        public Material laughStateMaterial;
        public float laughMinDuration = 2;
        public float laughMaxDuration = 6;
        public float noseAmplitude = 3;

        // Hit state
        public float hitSpeed = 20;
        public float hitDistance = 50;
        public Material hitSphereMaterial;

        // Patrol state
        public float patrolMinDistance = 5;
        public float patrolMaxDistance = 20;
        public float rotateMinDuration = 2;
        public float rotateMaxDuration = 7;


        // Start is called before the first frame update
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            renderer = GetComponent<Renderer>();

            // Set up components
            this.navMeshAgent.speed = this.movementSpeed;

            state = new PatrolState(this);
        }

        // Update is called once per frame
        private void Update()
        {
            state.Update();
        }

        private void OnCollisionStay(Collision collision)
        {
            state.OnCollision(collision);
        }

        public bool RabbitOnSight()
        {
            /*
             var extends = Vector3.one * boxcastSize;

            return Physics.BoxCast(transform.position + transform.forward * (extends / 2).magnitude, extends,
                transform.forward, transform.rotation,
                float.PositiveInfinity, rabbitLayerMask);
                */
            var raycastHits = Physics.RaycastAll(transform.position, transform.forward, float.PositiveInfinity,
                rabbitLayerMask);

            var (tag, _) = raycastHits
                .Select(x => (x.transform.tag, (x.transform.position - transform.position).magnitude))
                .Aggregate(("", float.PositiveInfinity), (acc, next) => acc.Item2 < next.Item2 ? acc : next);

            return tag == "Rabbit";
        }

        public Collider[] NearbyRabbitsDancing()
        {
            return Physics.OverlapSphere(transform.position, hearRadious, rabbitNoisesLayerMask);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.forward * hearRadious + transform.position);
            Gizmos.DrawWireSphere(transform.position, hearRadious);
        }
    }
}