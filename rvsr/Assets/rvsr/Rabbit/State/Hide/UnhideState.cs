using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class UnhideState: IRabbitState
    {
        private DodgeState dodgeState;

        public UnhideState(DodgeState dodgeState)
        {
            this.dodgeState = dodgeState;
            dodgeState.timer = 0;
        }
        
        public void Update()
        {
            dodgeState.rabbit.transform.position = Vector3.Lerp(dodgeState.finalPosition, dodgeState.initialPosition,
                this.dodgeState.rabbit.hideAnimationCurve.Evaluate(dodgeState.timer / this.dodgeState.rabbit.hideAnimationDuration));

            if (dodgeState.timer >= this.dodgeState.rabbit.hideAnimationDuration)
            {
                dodgeState.rabbit.state.Destroy();
                this.dodgeState.rabbit.state = new ShockState(dodgeState.rabbit);
            }
        }
    }
}