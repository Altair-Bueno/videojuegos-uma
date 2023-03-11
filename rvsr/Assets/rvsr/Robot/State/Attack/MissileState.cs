namespace rvsr.Robot.State.Attack
{
    public class MissileState : IRobotState
    {
        public AttackState attackState;
        public Robot robot;

        public MissileState(Robot robot, AttackState attackState)
        {
            this.robot = robot;
            this.attackState = attackState;
        }
    }
}