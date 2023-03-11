using rvsr.Robot.State;
using rvsr.Robot;
using rvsr.Robot.State.Hit;

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
            if (true)
            {
                // Escuchar sonido
            }
            else
            {
                // Golpear suelo
                robot.state = new HitState(robot);
            }
        }
    }
}