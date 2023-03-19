using UnityEngine;

namespace rvsr.Rabbit.State
{
    enum HideStatePhaseEnum
    {
        Hiding,
        Waiting,
        UnHiding
    }

    public class HideState : IRabbitState
    {
        public Rabbit rabbit;

        private float timer;

        private Vector3 initialPosition;
        private Vector3 finalPosition;

        private HideStatePhaseEnum hideStatePhaseEnum;


        public HideState(Rabbit rabbit)
        {
            this.rabbit = rabbit;
            
            hideStatePhaseEnum = HideStatePhaseEnum.Hiding;
            timer = 0;
            initialPosition = this.rabbit.transform.position;
            finalPosition = this.rabbit.transform.position + Vector3.down * 7;

            this.rabbit.collider.enabled = false;
            this.rabbit.rigidbody.detectCollisions = false;
            this.rabbit.rigidbody.useGravity = false;
        }

        public void Hide()
        {
            rabbit.transform.position = Vector3.Lerp(initialPosition, finalPosition,
                this.rabbit.hideAnimationCurve.Evaluate(timer / this.rabbit.hideAnimationDuration));

            if (timer >= this.rabbit.hideAnimationDuration)
            {
                timer = 0;
                hideStatePhaseEnum = HideStatePhaseEnum.Waiting;
            }
        }

        public void Waiting()
        {
            if (timer >= this.rabbit.hideDuration)
            {
                timer = 0;
                hideStatePhaseEnum = HideStatePhaseEnum.UnHiding;
            }
        }

        public void UnHide()
        {
            rabbit.transform.position = Vector3.Lerp(finalPosition, initialPosition,
                this.rabbit.hideAnimationCurve.Evaluate(timer / this.rabbit.hideAnimationDuration));

            if (timer >= this.rabbit.hideAnimationDuration)
            {
                Destroy();
                this.rabbit.state = new ShockState(rabbit);
            }
        }

        public void Update()
        {
            timer += Time.deltaTime;

            if (hideStatePhaseEnum == HideStatePhaseEnum.Hiding)
            {
                Hide();
            }
            else if (hideStatePhaseEnum == HideStatePhaseEnum.Waiting)
            {
                Waiting();
            }
            else if (hideStatePhaseEnum == HideStatePhaseEnum.UnHiding)
            {
                UnHide();
            }
        }

        public void Destroy()
        {
            this.rabbit.collider.enabled = true;
            this.rabbit.rigidbody.detectCollisions = true;
            this.rabbit.rigidbody.useGravity = true;
        }

        public void OnCollision(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("HitSphere"))
            {
                Debug.Log("Hit sphere detected");
                finalPosition = this.rabbit.transform.position;
                hideStatePhaseEnum = HideStatePhaseEnum.UnHiding;
                timer = 0;
            }
        }
    }
}