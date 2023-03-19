using rvsr.Robot.State.Patrol;
using UnityEngine;

namespace rvsr.Robot.State.Attack
{
    public class AttackState : IRobotState
    {
        public Robot robot;
        public IRobotState state;

        public AttackState(Robot robot)
        {
            Debug.Log("Robot: Attack");
            this.robot = robot;
            state = new MissileState(robot, this);
        }

        public void Update()
        {
            state.Update();

            if (robot.RabbitOnSight())
            {
                // NOP
            }
            else
            {
                robot.state.Destroy();
                robot.state = new PatrolState(robot);
            }
        }

        public void Destroy()
        {
            state.Destroy();
        }
    }
}