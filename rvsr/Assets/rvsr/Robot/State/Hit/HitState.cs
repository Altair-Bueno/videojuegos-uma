using rvsr.Robot.State.Patrol;

// TODO HitState

namespace rvsr.Robot.State.Hit
{
    public class HitState : IRobotState
    {
        public Robot robot;

        public HitState(Robot robot)
        {
            this.robot = robot;
        }

        public void Update()
        {
            if (false)
            {
                // TODO Golpe en el suelo
            }

            robot.state.Destroy();
            robot.state = new PatrolState(robot);
        }
    }
}