using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(
            PlayerMovementStateMachine stateMachine,
            EntityDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (input.MoveInput != Vector2.zero) stateMachine.ChangeState(stateMachine.MoveState);
        }
    }
}
