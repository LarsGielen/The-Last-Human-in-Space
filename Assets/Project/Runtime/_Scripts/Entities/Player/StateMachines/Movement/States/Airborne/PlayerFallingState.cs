using UnityEngine;

namespace Project.Player.Statemachine
{
    public class PlayerFallingState : PlayerAirborneState
    {
        public PlayerFallingState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            Movement.SetGravity(playerData.Gravity);
            animator.SetBool("FallingState", true);
        }

        public override void Exit()
        {
            base.Exit();

            animator.SetBool("FallingState", false);
        }

        public override void CheckTransitions()
        {
            if (Senses.CheckGrounded() && Movement.CurrentVerticalVelocity < 0.01f) stateMachine.ChangeState(stateMachine.LandingState);

            base.CheckTransitions();
        }
    }
}
