using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            movement.SetHorizontalMove(input.MoveInput, 0f, input.MoveRelativeToCamera);
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (input.MoveInput == Vector2.zero) return;

            if (input.Run) stateMachine.ChangeState(stateMachine.RunningState);
            else stateMachine.ChangeState(stateMachine.WalkingState);
        }
    }
}
