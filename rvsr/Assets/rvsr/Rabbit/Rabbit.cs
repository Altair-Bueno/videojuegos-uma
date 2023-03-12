using rvsr.Rabbit.State;
using UnityEngine;

namespace rvsr.Rabbit
{
    public class Rabbit : MonoBehaviour
    {
        public Animation animation;
        public LayerMask robotLayerMask;

        public float boxcastSize = 2;
        public float hideMinDuration = 2;
        public float hideMaxDuration = 7;
        public float unHideMinDuration = 1;
        public float unHideMaxDuration = 4;
        public IRabbitState state;

        // Start is called before the first frame update
        private void Start()
        {
            state = new DanceState(this);
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


        public bool RobotOnSight()
        {
            var extends = Vector3.one * boxcastSize;

            return Physics.BoxCast(transform.position + transform.forward * (extends / 2).magnitude, extends,
                transform.forward, transform.rotation,
                float.PositiveInfinity, robotLayerMask);
        }

        public bool MissileNearby()
        {
            // TODO missile nearby
            return false;
        }
    }
}