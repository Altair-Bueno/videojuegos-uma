using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class HideState : IRabbitState
    {
        private DodgeState dodgeState;

        public HideState(DodgeState dodgeState)
        {
            this.dodgeState = dodgeState;

            dodgeState.timer = 0;
            dodgeState.initialPosition = dodgeState.rabbit.transform.position;
            dodgeState.finalPosition = dodgeState.rabbit.transform.position + Vector3.down * 7;
        }

        public void Update()
        {
            dodgeState.rabbit.transform.position = Vector3.Lerp(dodgeState.initialPosition, dodgeState.finalPosition,
                this.dodgeState.rabbit.hideAnimationCurve.Evaluate(dodgeState.timer / this.dodgeState.rabbit.hideAnimationDuration));

            if (dodgeState.timer >= this.dodgeState.rabbit.hideAnimationDuration)
            {
                dodgeState.state.Destroy();
                dodgeState.state = new WaitState(dodgeState);
            }
        }
    }
}