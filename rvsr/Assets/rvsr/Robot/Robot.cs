using System;
using rvsr.Robot.State;
using rvsr.Robot.State.Hit;
using rvsr.Robot.State.Patrol;
using UnityEngine;
using UnityEngine.Serialization;

namespace rvsr.Robot
{
    public class Robot : MonoBehaviour
    {
        public IRobotState state;
        
        // Components
        public Rigidbody rigidbody;
        public Renderer renderer;
        
        // General variables
        public LayerMask rabbitLayerMask;
        public LayerMask rabbitNoisesLayerMask;
        public float boxcastSize = 2;
        public float movementSpeed = 10;
        public float hearRadious = 10;

        // Attack state
        public Missile missile;
        public Material laughStateMaterial;
        
        // Hit state
        public float hitSpeed = 20;
        public float hitDistance = 50;
        public Material hitSphereMaterial;

        // Patrol state
        public float patrolMinDistance = 5;
        public float patrolMaxDistance = 20;
        
        // Start is called before the first frame update
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            renderer = GetComponent<Renderer>();
            state = new PatrolState(this);
        }

        // Update is called once per frame
        private void Update()
        {
            state.Update();
        }

        private void OnCollisionEnter(Collision collision)
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
            return Physics.Raycast(transform.position, transform.forward, float.PositiveInfinity, rabbitLayerMask);
        }

        public Collider[] NearbyRabbitsDancing()
        {
            return Physics.OverlapSphere(transform.position, hearRadious, rabbitNoisesLayerMask);
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.forward * 10 + transform.position);
            Gizmos.DrawWireSphere(transform.position, hearRadious);
        }

        /*
        private void OnDrawGizmos()
        {
            var extends = Vector3.one * this.boxcastSize * 2;

            Gizmos.DrawWireCube(this.transform.position + transform.forward * (extends).magnitude / 2, extends);
            //Gizmos.DrawWireSphere(this.transform.position, boxcastSize);
        }*/
    }
}