using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class DodgeState : IRabbitState
    {
        public IRabbitState state;
        public Rabbit rabbit;

        public float timer;
        public Vector3 initialPosition;
        public Vector3 finalPosition;
        
        public DodgeState(Rabbit rabbit)
        {
            this.rabbit = rabbit;
            
            this.rabbit.rigidbody.detectCollisions = false;
            this.rabbit.rigidbody.useGravity = false;

            state = new HideState(this);
        }
        
        public void Update()
        {
            timer += Time.deltaTime;
            state.Update();
        }

        public void Destroy()
        {
            this.rabbit.rigidbody.detectCollisions = true;
            this.rabbit.rigidbody.useGravity = true;
        }

        public void OnCollision(Collision collision)
        {
            state.OnCollision(collision);
            if (collision.gameObject.layer == LayerMask.NameToLayer("HitSphere"))
            {
                Debug.Log("Hit sphere detected");
                finalPosition = this.rabbit.transform.position;
                state.Destroy();
                state = new UnhideState(this);
            }
        }
    }
}