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
            this.robot = robot;
            state = new MissileState(robot, this);
        }

        public void Update()
        {
            state.Update();

            if (robot.rabbitOnSight())
            {
                // Contacto visual con enemigo
                // NOPE
            }
            else
            {
                // Otherwise
                Destroy();
                robot.state = new PatrolState(robot);
            }
        }

        public void Destroy()
        {
            state.Destroy();
        }
    }
}