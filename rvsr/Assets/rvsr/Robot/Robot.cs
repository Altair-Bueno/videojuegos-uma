using System;
using UnityEngine;
using rvsr.Robot.State;
using rvsr.Robot.State.Patrol;

namespace rvsr.Robot
{
    public class Robot : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public LayerMask layerMask;
        public float speed = 10;
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

        public bool rabbitOnSight()
        {
            var extends = Vector3.one * this.boxcastSize;

            return Physics.BoxCast(this.transform.position + transform.forward * (extends / 2).magnitude, extends,
                this.transform.forward, this.transform.rotation,
                float.PositiveInfinity, this.layerMask);
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