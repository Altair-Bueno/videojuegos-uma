using rvsr.Robot.State.Attack;
using rvsr.Robot.State.Walk;

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

            if (robot.RabbitOnSight())
            {
                robot.state.Destroy();
                robot.state = new AttackState(robot);
            }
            else if (robot.RabbitNoisesNearby())
            {
                robot.state.Destroy();
                robot.state = new WalkState(robot);
            }
        }

        public void Destroy()
        {
            state.Destroy();
        }
    }
}