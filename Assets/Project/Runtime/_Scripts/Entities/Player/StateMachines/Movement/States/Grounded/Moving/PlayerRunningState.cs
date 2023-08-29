using UnityEngine;

namespace Project.Player.Statemachine
{
    public class PlayerRunningState : PlayerGroundedState
    {
        public PlayerRunningState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void StateUpdate()
        {
            base.StateUpdate();

            movement.SetHorizontalMove(input.MoveInput, playerData.RunSpeed, input.MoveRelativeToCamera);
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (input.MoveInput == Vector2.zero && movement.CurrentMoveSpeed == 0) stateMachine.ChangeState(stateMachine.IdleState);
            else if (!input.Run) stateMachine.ChangeState(stateMachine.WalkingState);
        }
    }
}
