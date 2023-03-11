using UnityEngine;


namespace rvsr.Robot.State.Patrol
{
    public class RotateState : IRobotState
    {
        public PatrolState patrolState;
        public Robot robot;
        public float direction;
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
            robot.transform.Rotate(Vector3.up * direction, Time.deltaTime * robot.speed);

            timer -= Time.deltaTime;
            if (timer <= 0) patrolState.state = new ForwardState(robot, patrolState);
        }
    }
}