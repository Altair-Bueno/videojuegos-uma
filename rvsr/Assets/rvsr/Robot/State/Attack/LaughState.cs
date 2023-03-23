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
            Debug.Log("Robot: Laugh");
            this.robot = robot;
            this.attackState = attackState;
            timer = Random.Range(robot.laughMinDuration,robot.laughMaxDuration);

            oldMaterial = this.robot.renderer.material;
            robot.renderer.material = robot.laughStateMaterial;
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            robot.nose.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.Sin(timer * robot.movementSpeed) * robot.noseAmplitude,0,0)); 

            if (timer <= 0)
            {
                Destroy();
                attackState.state = new MissileState(robot, attackState);
            }
        }

        public void Destroy()
        {
            robot.nose.transform.localRotation = Quaternion.identity;
            robot.renderer.material = oldMaterial;
        }
    }
}