using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace rvsr.Robot.State.Attack
{
    public class LaughState : IRobotState
    {
        private AttackState attackState;
        private Robot robot;

        private Material oldMaterial;
        private float timer;

        public LaughState(Robot robot, AttackState attackState)
        {
            Debug.Log("Laugh");
            this.robot = robot;
            this.attackState = attackState;
            timer = Random.Range(2,4);

            oldMaterial = this.robot.renderer.material;
            robot.renderer.material = robot.laughStateMaterial;
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Destroy();
                attackState.state = new MissileState(robot, attackState);
            }
        }

        public void Destroy()
        {
            robot.renderer.material = oldMaterial;
        }
    }
}