using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(
            PlayerMovementStateMachine stateMachine,
            EntityDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            animator.SetTrigger("LandingState");

            if (movement.MoveSpeed == 0) stateMachine.ChangeState(stateMachine.IdleState);
            else stateMachine.ChangeState(stateMachine.MoveState);
        }
    }
}
