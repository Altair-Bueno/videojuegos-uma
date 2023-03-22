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
            Debug.Log("Robot: Rotate");
            this.robot = robot;
            this.patrolState = patrolState;

            timer = Random.Range(robot.rotateMinDuration, robot.rotateMaxDuration);
            direction = Random.Range(0, 2) == 0 ? 1 : -1;
        }

        public void Update()
        {
            robot.rigidbody.MoveRotation(robot.rigidbody.rotation * Quaternion.AngleAxis(Time.deltaTime * robot.movementSpeed * direction, Vector3.up));
            //robot.transform.Rotate(Vector3.up * direction, Time.deltaTime * robot.movementSpeed);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                patrolState.state.Destroy();
                patrolState.state = new ForwardState(robot, patrolState);
            }
        }
    }
}