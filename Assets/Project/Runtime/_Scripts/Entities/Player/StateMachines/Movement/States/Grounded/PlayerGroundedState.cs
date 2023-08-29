using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public abstract class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(
            PlayerMovementStateMachine stateMachine,
            PlayerDataSO playerData,
            PlayerInput input,
            Animator animator) : base(stateMachine, playerData, input, animator)
        { }

        public override void Enter()
        {
            base.Enter();

            // Set gravity to 0 and set small constant down force
            movement.SetGravity(0);
            movement.SetVerticalVelocity(-playerData.GroundedGravity);
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (input.Jump) stateMachine.ChangeState(stateMachine.JumpingState);
            else if (senses.CheckGrounded() == false) stateMachine.ChangeState(stateMachine.FallingState);

            // Abilities
            else if (input.Attack) stateMachine.ChangeState(stateMachine.AttackingState); 
        }
    }
}
