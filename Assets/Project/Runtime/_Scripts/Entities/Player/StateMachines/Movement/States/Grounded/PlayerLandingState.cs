using UnityEngine;

namespace Project.Player.Statemachine
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            animator.SetTrigger("LandingState");
            stateMachine.ChangeState(stateMachine.WalkingState);
        }
    }
}
