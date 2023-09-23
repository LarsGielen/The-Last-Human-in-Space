using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public class PlayerInAirState : PlayerMovementState
    {
        public PlayerInAirState(
            PlayerMovementStateMachine stateMachine,
            EntityDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            animator.SetBool("InAir", true);
            movement.SetGravity(playerData.Gravity);
        }

        public override void Exit()
        {
            base.Exit();

            animator.SetBool("InAir", false);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            float speed;
            if (input.MoveInput == Vector2.zero) speed = 0f;
            else if (!input.Run) speed = playerData.WalkSpeed;
            else speed = playerData.RunSpeed;

            movement.SimpleMove(input.MoveInput, speed, input.MoveRelativeToCamera);
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (senses.CheckGrounded() && movement.VerticalVelocity < 0.01f) stateMachine.ChangeState(stateMachine.LandingState);
        }
    }
}
