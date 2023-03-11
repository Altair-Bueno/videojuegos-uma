using rvsr.Robot.State.Patrol;

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
            if (false) robot.state = new PatrolState(robot);
        }
    }
}