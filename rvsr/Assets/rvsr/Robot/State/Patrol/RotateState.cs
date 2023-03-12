using UnityEngine;

namespace rvsr.Robot.State.Patrol
{
    public class RotateState : IRobotState
    {
        public float direction;
        public PatrolState patrolState;
        public Robot robot;

        private float timer;

        public RotateState(Robot robot, PatrolState patrolState)
        {
            this.robot = robot;
            this.patrolState = patrolState;

            timer = Random.Range(2, 10);
            direction = Random.Range(0, 2) == 0 ? 1 : -1;
        }

        public void Update()
        {
            robot.transform.Rotate(Vector3.up * direction, Time.deltaTime * robot.movementSpeed);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                patrolState.state.Destroy();
                patrolState.state = new ForwardState(robot, patrolState);
            }
        }
    }
}