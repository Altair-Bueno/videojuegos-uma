using rvsr.Rabbit.State;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace rvsr.Rabbit
{
    public class Rabbit : MonoBehaviour
    {
        public IRabbitState state;
        
        // Components
        public Animation animation;
        public Collider collider;
        public Rigidbody rigidbody;
        public NavMeshAgent navMeshAgent;

        
        // General variables
        public LayerMask robotLayerMask;
        public LayerMask missileLayerMask;
        public float movementSpeed;
        public float hearRadious = 10;
        public GameObject[] campFires;
        
        // Shock state
        public float shockMinDuration = 1;
        public float shockMaxDuration = 4;
        // Hide state
        public float hideDuration = 3;
        public float hideAnimationDuration = 0.35f;
        public AnimationCurve hideAnimationCurve;
        // Scape state
        [Range(0,100)]
        public int dodgeLuck = 10;

        // Start is called before the first frame update
        private void Start()
        {
            state = new DanceState(this);

            navMeshAgent.speed = movementSpeed;
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
            Gizmos.color = Color.magenta;
            //Gizmos.DrawLine(transform.position, transform.forward * 10 + transform.position);
            Gizmos.DrawWireSphere(transform.position, hearRadious);
        }

    }
}