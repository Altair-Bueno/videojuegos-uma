using System;

namespace rvsr.Robot.State.Attack
{
    public class LaughState : IRobotState
    {
        private AttackState attackState;
        private Robot robot;


        public LaughState(Robot robot, AttackState attackState)
        {
            this.robot = robot;
            this.attackState = attackState;
            // TODO Add mesh
        }

        public void Update(Robot robot)
        {
            throw new NotImplementedException();
        }

        public void Destroy(Robot robot)
        {
            // TODO remove mesh
        }
    }
}