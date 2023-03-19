using UnityEngine;

namespace rvsr.Robot.State.Attack
{
    public class MissileState : IRobotState
    {
        public AttackState attackState;

        private readonly Missile missile;
        public Robot robot;

        public MissileState(Robot robot, AttackState attackState)
        {
            Debug.Log("Robot: Missile");
            this.robot = robot;
            this.attackState = attackState;

            var pos = robot.transform.position + robot.transform.forward * 2.5f + Vector3.up * 0.2f;
            var rot = robot.transform.rotation;
            missile = Object.Instantiate(this.robot.missile, pos, rot);
            missile.missileState = this;
        }

        public void Destroy()
        {
            Object.Destroy(missile.gameObject);
        }

        public void OnRabbitMissileHit(Rabbit.Rabbit rabbit)
        {
            attackState.state.Destroy();
            attackState.state = new LaughState(robot, attackState);
        }

        public void OnMissileHitFallback()
        {
            attackState.state.Destroy();
            attackState.state = new MissileState(robot, attackState);
        }
    }
}