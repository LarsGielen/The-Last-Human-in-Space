using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void StateUpdate()
        {
            base.StateUpdate();

            float speed = input.Run ? playerData.RunSpeed : playerData.WalkSpeed;

            movement.SetHorizontalMove(input.MoveInput, speed, input.MoveRelativeToCamera);
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (input.MoveInput == Vector2.zero && movement.CurrentMoveSpeed == 0) stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
