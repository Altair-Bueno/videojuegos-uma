using UnityEngine;


namespace rvsr.Robot.State.Patrol
{
    public class ForwardState : IRobotState
    {
        private float distance;
        public PatrolState patrolState;
        public Robot robot;

        public ForwardState(Robot robot, PatrolState patrolState)
        {
            this.robot = robot;
            this.patrolState = patrolState;
            distance = Random.Range(5, 15);
        }

        public void Update()
        {
            var moved = robot.speed * Time.deltaTime * robot.transform.forward;
            robot.rigidbody.MovePosition(robot.transform.position + moved);

            distance -= moved.magnitude;

            if (distance <= 0) patrolState.state = new RotateState(robot, patrolState);
        }

        public void OnCollision(Collision collision)
        {
            // Floor is tagged as "Terrain"
            if (collision.gameObject.CompareTag("Terrain")) return;

            // Stop movement if we reach a wall
            patrolState.state = new RotateState(robot, patrolState);
        }
    }
}