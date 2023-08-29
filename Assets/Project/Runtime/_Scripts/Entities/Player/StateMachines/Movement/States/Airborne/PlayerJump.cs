using UnityEngine;

namespace Project.Player.Statemachine
{
    public class PlayerJump : PlayerAirborneState
    {
        private float modifiedGravity;
        private bool isFalling;

        public PlayerJump(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            isFalling = false;
            OnJump();
        }

        public override void Exit()
        {
            base.Exit();

            input.Jump = false;
            animator.SetBool("JumpState", false);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            if (movement.CurrentVerticalVelocity < 0.01f && !isFalling) OnFalling();
        }

        public override void CheckTransitions()
        {
            if (playerData.JumpTime < Time.time - stateStartTime) stateMachine.ChangeState(stateMachine.FallingState);
            else if (senses.CheckGrounded() && isFalling) stateMachine.ChangeState(stateMachine.LandingState);

            base.CheckTransitions();
        }

        private void OnJump()
        {
            float maxJumpHeight = playerData.JumpHeight;
            float timeToApex = playerData.JumpTime / 4.0f;
            modifiedGravity = (maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            float initialJumpVelocity = (maxJumpHeight) / timeToApex;

            movement.SetGravity(modifiedGravity);
            movement.SetVerticalVelocity(initialJumpVelocity);

            animator.SetBool("JumpState", true);
        }

        private void OnFalling()
        {
            movement.SetGravity(modifiedGravity * playerData.GravityMultiplierAfterJump);
            isFalling = true;
        }
    }
}
