using UnityEngine;

namespace Project.Entity.Player.Statemachine
{
    public abstract class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(
            PlayerMovementStateMachine stateMachine,
            EntityDataSO playerData,
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

        public override void StateUpdate()
        {
            base.StateUpdate();

            
        }

        public override void CheckTransitions()
        {
            base.CheckTransitions();

            if (senses.CheckGrounded() == false) stateMachine.ChangeState(stateMachine.InAirState);
        }
    }
}
