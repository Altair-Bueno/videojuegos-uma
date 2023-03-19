using rvsr.Robot.State.Attack;
using rvsr.Robot.State.Hit;
using UnityEngine;

namespace rvsr.Robot.State.Walk
{
    public class WalkState : IRobotState
    {
        public Robot robot;

        public WalkState(Robot robot)
        {
            Debug.Log("Walk");
            this.robot = robot;
        }

        public void Update()
        {
            var nearbyRabbitsDancing = robot.NearbyRabbitsDancing();
            if (robot.RabbitOnSight())
            {
                Destroy();
                robot.state = new AttackState(robot);
            }
            else if (nearbyRabbitsDancing.Length != 0)
            {
                var rabbitCollider = nearbyRabbitsDancing[0];
                robot.navMeshAgent.SetDestination(rabbitCollider.transform.position);
            }
            else
            {
                Destroy();
                robot.state = new HitState(robot);
            }
        }

        public void Destroy()
        {
            robot.navMeshAgent.isStopped = true;
        }
    }
}