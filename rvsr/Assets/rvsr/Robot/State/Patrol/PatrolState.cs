using rvsr.Robot.State.Attack;
using rvsr.Robot.State.Walk;
using UnityEngine;


namespace rvsr.Robot.State.Patrol
{
    public class PatrolState : IRobotState
    {
        public Robot robot;
        public IRobotState state;

        public PatrolState(Robot robot)
        {
            this.robot = robot;
            state = new RotateState(robot, this);
        }

        public void Update()
        {
            state.Update();

            if (robot.rabbitOnSight())
            {
                // Contacto visual con enemigo
                Destroy();
                robot.state = new AttackState(robot);
            }
            else if (false) // TODO condicion de walk state
            {
                // Escuchar sonido
                Destroy();
                robot.state = new WalkState(robot);
            }
        }

        public void Destroy()
        {
            state.Destroy();
        }
    }
}