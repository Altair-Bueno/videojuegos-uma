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
            Debug.Log("Robot: Fordward");
            this.robot = robot;
            this.patrolState = patrolState;
            distance = Random.Range(this.robot.patrolMinDistance, this.robot.patrolMaxDistance);
        }

        public void Update()
        {
            var moved = robot.movementSpeed * Time.deltaTime * robot.transform.forward;
            robot.rigidbody.MovePosition(robot.transform.position + moved);

            distance -= moved.magnitude;

            if (distance <= 0)
            {
                patrolState.state.Destroy();
                patrolState.state = new RotateState(robot, patrolState);
            }
        }

        public void OnCollision(Collision collision)
        {
            // Floor is tagged as "Terrain"
            if (collision.gameObject.CompareTag("Terrain")) return;

            // Stop movement if we reach a wall
            robot.rigidbody.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            robot.rigidbody.MovePosition(robot.transform.position + Vector3.forward * .3f);
            patrolState.state.Destroy();
            patrolState.state = new RotateState(robot, patrolState);
        }
    }
}