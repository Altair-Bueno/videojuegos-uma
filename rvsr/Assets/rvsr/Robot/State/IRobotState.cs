using UnityEngine;

namespace rvsr.Robot.State
{
    public interface IRobotState
    {
        public void Update()
        {
        }

        public void OnCollision(Collision collision)
        {
        }

        public void Destroy()
        {
        }
    }
}