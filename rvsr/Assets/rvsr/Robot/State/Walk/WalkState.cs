using rvsr.Robot.State.Hit;

// TODO pathfinding

namespace rvsr.Robot.State.Walk
{
    public class WalkState : IRobotState
    {
        public Robot robot;

        public WalkState(Robot robot)
        {
            this.robot = robot;
        }

        public void Update()
        {
            // TODO Arrive to destination using pathfinding

            if (robot.RabbitNoisesNearby())
                robot.state = new WalkState(robot);
            else
                robot.state = new HitState(robot);
        }
    }
}