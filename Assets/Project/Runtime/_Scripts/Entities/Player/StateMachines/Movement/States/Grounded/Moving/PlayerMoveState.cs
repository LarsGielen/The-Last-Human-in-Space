using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(
            PlayerMovementStateMachine stateMachine,
            EntityDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void StateUpdate()
        {
            base.StateUpdate();

            float speed = input.Run ? playerData.RunSpeed : playerData.WalkSpeed;

            movement.SimpleMove(input.MoveInput, speed, input.MoveRelativeToCamera);
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (input.MoveInput == Vector2.zero && movement.MoveSpeed == 0) stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
