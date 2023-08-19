using UnityEngine;

namespace Project.StateMachine.Player
{
    public class PlayerJump : PlayerAirborneState
    {
        private float modifiedGravity;
        private bool isFalling;

        public PlayerJump(PlayerMovementStateMachine stateMachine, PlayerData playerData) : base(stateMachine, playerData)
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

            playerInput.Jump = false;
            animator.SetBool("JumpState", false);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            if (playerData.CurrentYVelocity < 0.01f && !isFalling) OnFalling();
        }

        public override void CheckTransitions()
        {
            if (playerData.JumpTime < Time.time - stateStartTime) stateMachine.ChangeState(stateMachine.FallingState);
            else if (CheckGrounded() && isFalling) stateMachine.ChangeState(stateMachine.LandingState);

            base.CheckTransitions();
        }

        private void OnJump()
        {
            float maxJumpHeight = playerData.JumpHeight;
            float timeToApex = playerData.JumpTime / 2f;
            modifiedGravity = (2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
            float initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;

            playerData.CurrentGravity = modifiedGravity;
            playerData.CurrentYVelocity = initialJumpVelocity;

            animator.SetBool("JumpState", true);
        }

        private void OnFalling()
        {
            playerData.CurrentGravity = modifiedGravity * playerData.GravityMultiplierAfterJump;
            isFalling = true;
        }
    }
}
