using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerJump : PlayerAbilityState
    {
        private bool isFalling;

        public PlayerJump(
            PlayerAbilityStateMachine stateMachine,
            EntityDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            stateMachine.Player.LockStateMachine<PlayerMovementStateMachine>(true);

            isFalling = false;
            OnJump();
        }

        public override void Exit()
        {
            base.Exit();

            var movementStatemachine = stateMachine.Player.LockStateMachine<PlayerMovementStateMachine>(false);
            movementStatemachine.ChangeState(movementStatemachine.InAirState);

            animator.SetBool("JumpState", false);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            float speed = input.Run ? playerData.RunSpeed : playerData.WalkSpeed;
            movement.SimpleMove(input.MoveInput, speed, input.MoveRelativeToCamera);

            if (movement.VerticalVelocity < 0.01f && !isFalling) OnFalling();
        }

        public override void CheckTransitions()
        {
            if (senses.CheckGrounded() && isFalling) stateMachine.ChangeState(stateMachine.ListenState);
            else if (playerData.JumpTime < Time.time - stateStartTime) stateMachine.ChangeState(stateMachine.ListenState); 

            base.CheckTransitions();
        }

        private void OnJump()
        {
            movement.SimpleJump(playerData.JumpHeight);

            animator.SetBool("JumpState", true);
        }

        private void OnFalling()
        {
            movement.SetGravity(movement.Gravity * playerData.GravityJumpMultiplier);
            isFalling = true;
        }
    }
}
