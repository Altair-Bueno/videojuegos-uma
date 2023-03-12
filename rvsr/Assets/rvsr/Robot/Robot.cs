using rvsr.Robot.State;
using rvsr.Robot.State.Patrol;
using UnityEngine;

namespace rvsr.Robot
{
    public class Robot : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public LayerMask layerMask;
        public Missile missile;

        public float movementSpeed = 10;
        public float patrolMinDistance = 5;
        public float patrolMaxDistance = 20;
        public float boxcastSize = 2;

        public IRobotState state;

        // Start is called before the first frame update
        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
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
            var extends = Vector3.one * boxcastSize;

            return Physics.BoxCast(transform.position + transform.forward * (extends / 2).magnitude, extends,
                transform.forward, transform.rotation,
                float.PositiveInfinity, layerMask);
        }

        public bool RabbitNoisesNearby()
        {
            // TODO rabbit noises
            return false;
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