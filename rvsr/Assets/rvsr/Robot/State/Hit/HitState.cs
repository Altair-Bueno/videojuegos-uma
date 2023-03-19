using rvsr.Robot.State.Patrol;
using UnityEngine;

namespace rvsr.Robot.State.Hit
{
    public class HitState : IRobotState
    {
        public Robot robot;

        private GameObject sphere;

        public HitState(Robot robot)
        {
            Debug.Log("Robot: Hit");
            this.robot = robot;
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.layer = LayerMask.NameToLayer("HitSphere");
            sphere.gameObject.GetComponent<Renderer>().material = robot.hitSphereMaterial;
            sphere.transform.position = robot.transform.position + Vector3.down * 0.75f;
            sphere.transform.localScale = Vector3.zero;
        }

        public void Update()
        {
            sphere.transform.localScale += robot.hitSpeed * Time.deltaTime * Vector3.one;

            if (sphere.transform.localScale.magnitude >= robot.hitDistance)
            {
                robot.state.Destroy();
                robot.state = new PatrolState(robot);
            }
        }

        public void Destroy()
        {
            GameObject.Destroy(sphere);
        }
    }
}