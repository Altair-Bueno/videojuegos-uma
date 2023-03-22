using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class DanceState : IRabbitState
    {
        public Rabbit rabbit;

        private GameObject rabbitDance;

        public DanceState(Rabbit rabbit)
        {
            Debug.Log("Rabbit Dance");
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
            if (rabbit.RobotNearby() || rabbit.MissileNearby())
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
        
        public void OnCollision(Collision collision)
        {
            var hitSphereLayer = LayerMask.NameToLayer("HitSphere");
            var missileLayer = LayerMask.NameToLayer("Missile");
            
            if (collision.gameObject.layer == hitSphereLayer || collision.gameObject.layer == missileLayer)
            {
                this.rabbit.state.Destroy();
                rabbit.state = new ShockState(rabbit);
            }
        }
    }
}