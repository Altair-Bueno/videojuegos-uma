using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class DanceState : IRabbitState
    {
        public Rabbit rabbit;

        private GameObject rabbitDance;

        public DanceState(Rabbit rabbit)
        {
            this.rabbit = rabbit;
            this.rabbit.animation.enabled = true;
            rabbitDance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            rabbitDance.layer = LayerMask.NameToLayer("RabbitDance");
            rabbitDance.name = "RabbitDance";
            rabbitDance.transform.position = rabbit.transform.position;
            rabbitDance.transform.localScale = .01f * Vector3.one;
            rabbitDance.GetComponent<Renderer>().enabled = false;
            rabbitDance.transform.SetParent(rabbit.transform);
        }

        public void Update()
        {
            if (rabbit.RobotOnSight())
            {
                Destroy();
                rabbit.state = new ScapeState(rabbit);
            }
        }

        public void Destroy()
        {
            GameObject.Destroy(rabbitDance);
            rabbit.animation.enabled = false;
        }
    }
}