using rvsr.Rabbit.State;
using UnityEngine;
using UnityEngine.Serialization;

namespace rvsr.Rabbit
{
    public class Rabbit : MonoBehaviour
    {
        public Animation animation;
        public Collider collider;
        public Rigidbody rigidbody;
        
        public LayerMask robotLayerMask;
        public LayerMask missileLayerMask;

        public float movementSpeed;
        public float hearRadious = 10;
        public float hideDuration = 3;
        public float shockMinDuration = 1;
        public float shockMaxDuration = 4;
        public float hideAnimationDuration = 0.35f;
        public AnimationCurve hideAnimationCurve;
        public IRabbitState state;

        // Start is called before the first frame update
        private void Start()
        {
            //state = new DanceState(this);
            state = new ScapeState(this);
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

        public Collider[] NearbyRobots()
        {
            return Physics.OverlapSphere(this.transform.position, hearRadious, robotLayerMask);
        }


        public bool RobotNearby()
        {
            return NearbyRobots().Length > 0;
        }

        public bool MissileNearby()
        {
            return Physics.OverlapSphere(this.transform.position, hearRadious, missileLayerMask).Length > 0;
        }
        
        public void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.forward * 10 + transform.position);
            Gizmos.DrawWireSphere(transform.position, hearRadious);
        }

    }
}