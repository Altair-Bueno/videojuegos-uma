using rvsr.Robot.State.Attack;
using rvsr.Robot.State.Hit;
using UnityEngine;

// TODO pathfinding

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
            // TODO Arrive to destination using pathfinding
            if (robot.RabbitOnSight())
            {
                Destroy();
                robot.state = new AttackState(robot);
            }
            else if (robot.NearbyRabbitsDancing().Length != 0)
            {
                // NOP
            }
            else
            {
                Destroy();
                robot.state = new HitState(robot);
            }
        }

        public void Destroy()
        {
        }
    }
}