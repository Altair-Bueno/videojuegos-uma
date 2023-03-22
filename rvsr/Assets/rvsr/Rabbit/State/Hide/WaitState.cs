using UnityEngine;

namespace rvsr.Rabbit.State
{
    public class WaitState : IRabbitState
    {
        public DodgeState dodgeState;
        
        public WaitState(DodgeState dodgeState)
        {
            this.dodgeState = dodgeState;
            
            dodgeState.timer = 0;
        }


        public void Update()
        {
            if (dodgeState.timer >= dodgeState.rabbit.hideDuration)
            {
                dodgeState.state.Destroy();
                dodgeState.state = new UnhideState(dodgeState);
            }
        }
    }
}